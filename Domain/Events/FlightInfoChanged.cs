
namespace Domain.Events;

public record FlightInfoChanged(string Id, string FlightId, string Airport, string Airline, DateTime ScheduleTime,
    FlightType Type, string SourceOrDestinationAirport,
    FlightStatus? Status) : IEvent;