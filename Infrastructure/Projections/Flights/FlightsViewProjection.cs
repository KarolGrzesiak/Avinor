using Application.Flights.Queries;
using Domain;
using Domain.Events;
using Marten.Events.Projections;

namespace Infrastructure.Projections.Flights;

public class FlightsViewProjection : MultiStreamProjection<FlightsView, string>
{
    public FlightsViewProjection()
    {
        Identity<FlightArrived>(e => e.Airport);
        Identity<FlightCancelled>(e => e.Airport);
        Identity<FlightDeparted>(e => e.Airport);
        Identity<FlightInfoChanged>(e => e.Airport);
        Identity<FlightTimeChanged>(e => e.Airport);
        Identity<FlightCreated>(e => e.Airport);
    }

    public void Apply(FlightCreated e, FlightsView f)
    {
        f.Airport = e.Airport;
        f.Flights.Add(new FlightView(e));
    }

    public void Apply(FlightArrived e, FlightsView f) =>
        f.Flights.First(f => f.Id == e.Id).Status = FlightStatus.Arrived;

    public void Apply(FlightCancelled e, FlightsView f) =>
        f.Flights.First(f => f.Id == e.Id).Status = FlightStatus.Canceled;

    public void Apply(FlightDeparted e, FlightsView f) =>
        f.Flights.First(f => f.Id == e.Id).Status = FlightStatus.Departed;

    public void Apply(FlightInfoChanged e, FlightsView f)
    {
        var flight = f.Flights.First(f => f.Id == e.Id);
        flight.FlightId = e.FlightId;
        flight.Status = e.Status;
        flight.DestinationOrSourceAirport = e.SourceOrDestinationAirport;
        flight.Type = e.Type;
        flight.Date = e.ScheduleTime.ToShortDateString();
        flight.Time = e.ScheduleTime.ToShortTimeString();
        flight.AirLine = e.Airline;
    }

    public void Apply(FlightTimeChanged e, FlightsView f)
    {
        var flight = f.Flights.First(f => f.Id == e.Id);

        flight.Date = e.ScheduleTime.ToShortDateString();
        flight.Time = e.ScheduleTime.ToShortTimeString();
    }
}