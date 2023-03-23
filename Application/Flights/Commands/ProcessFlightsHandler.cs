using Application.Flights.Events;
using Domain;
using Domain.Events;
using Marten;
using MediatR;

namespace Application.Flights.Commands;

public class ProcessFlightsHandler : IRequestHandler<ProcessFlights>
{
    private readonly IDocumentSession _session;
    private readonly IMediator _mediator;

    public ProcessFlightsHandler(IDocumentSession session, IMediator mediator)
    {
        _session = session;
        _mediator = mediator;
    }

    public async Task Handle(ProcessFlights request, CancellationToken cancellationToken)
    {
        foreach (var flight in request.Flights)
        {
            var metadata = await _session.Events.FetchStreamStateAsync(flight.Id.ToString(), cancellationToken);
            if (metadata is null)
            {
                await _mediator.Publish(new FlightCreated(flight.Id, flight.FlightId, request.Airport, flight.Airline,
                    flight.ScheduleTime, flight.Type,
                    flight.SourceOrDestinationAirport, flight.Status), cancellationToken);
            }
            else if (flight.Status is not null)
            {
                INotification @event = flight.Status switch
                {
                    FlightStatus.Arrived => new FlightArrived(flight.Id, flight.FlightId, request.Airport),
                    FlightStatus.Canceled => new FlightCancelled(flight.Id, flight.FlightId, request.Airport),
                    FlightStatus.Departed => new FlightDeparted(flight.Id, flight.FlightId, request.Airport),
                    FlightStatus.NewInfo => new FlightInfoChanged(flight.Id, flight.FlightId, request.Airport,
                        flight.Airline, flight.ScheduleTime,
                        flight.Type,
                        flight.SourceOrDestinationAirport, flight.Status),
                    FlightStatus.NewTime => new FlightTimeChanged(flight.Id, flight.FlightId, request.Airport,
                        flight.ScheduleTime)
                };
                await _mediator.Publish(@event, cancellationToken);
            }
        }
    }
}