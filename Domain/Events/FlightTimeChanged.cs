using MediatR;

namespace Domain.Events;

public record FlightTimeChanged(long Id, string FlightId, string Airport, DateTime ScheduleTime) : INotification;