using Domain;
using Domain.Events;

namespace Application.Flights.Queries;

public class FlightView
{
    public FlightView()
    {
    }

    public FlightView(FlightCreated @event)
    {
        AirLine = @event.Airline;
        DestinationOrSourceAirport = @event.DestinationOrSourceAirport;
        Date = @event.ScheduleTime.ToShortDateString();
        FlightId = @event.FlightId;
        Status = @event.Status;
        Time = @event.ScheduleTime.ToShortTimeString();
        Type = @event.Type;
        Id = @event.Id;
    }

    public string Id { get; set; }
    public string DestinationOrSourceAirport { get; set; }
    public string Date { get; set; }
    public string AirLine { get; set; }
    public string FlightId { get; set; }
    public string Time { get; set; }
    public FlightType Type { get; set; }
    public FlightStatus? Status { get; set; }
}