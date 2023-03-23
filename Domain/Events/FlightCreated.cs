
namespace Domain.Events;

public record FlightCreated(string Id, string FlightId, string Airport, string Airline, DateTime ScheduleTime, FlightType Type, string DestinationOrSourceAirport,
    FlightStatus? Status) : IEvent;