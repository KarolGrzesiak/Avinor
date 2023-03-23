using Application.Common;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Flights.Commands;

public class ProcessFlightsHandler : IRequestHandler<ProcessFlights>
{
    private readonly IFlightsRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public ProcessFlightsHandler(IFlightsRepository repository, IEventProcessor eventProcessor)
    {
        _repository = repository;
        _eventProcessor = eventProcessor;
    }

    public async Task Handle(ProcessFlights request, CancellationToken cancellationToken)
    {
        foreach (var flight in request.Flights)
        {
            var existingFlight = await _repository.LoadAsync(flight.UniqueId.ToString(), null, cancellationToken);
            if (existingFlight is null)
            {
                existingFlight = new Flight(flight.UniqueId.ToString(), flight.FlightId, request.Airport,
                    flight.AirlineCode,
                    flight.ScheduleTime,
                    flight.ArrDep.MapFlightType(),
                    flight.SourceOrDestinationAirport, flight.StatusDto.MapFlightStatus());
            }
            else if (flight.StatusDto is not null)
            {
                switch (flight.StatusDto.MapFlightStatus())
                {
                    case FlightStatus.Arrived:
                        existingFlight.SetAsArrived();
                        break;
                    case FlightStatus.Canceled:
                        existingFlight.Cancel();
                        break;
                    case FlightStatus.Departed:
                        existingFlight.SetAsDeparted();
                        break;
                    case FlightStatus.NewInfo:
                        existingFlight.ChangeInfo(flight.FlightId, request.Airport, flight.AirlineCode,
                            flight.ScheduleTime, flight.ArrDep.MapFlightType(), FlightStatus.NewInfo,
                            flight.SourceOrDestinationAirport);
                        break;
                    case FlightStatus.NewTime:
                        existingFlight.ChangeTime(flight.ScheduleTime);
                        break;
                }
            }

            await _eventProcessor.ProcessAsync(existingFlight, cancellationToken);
        }
    }
}