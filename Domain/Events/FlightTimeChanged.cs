namespace Domain.Events;

public record FlightTimeChanged(string Id, string FlightId, string Airport, DateTime ScheduleTime) : IEvent;