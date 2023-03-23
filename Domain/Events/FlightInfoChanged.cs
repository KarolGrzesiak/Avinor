using MediatR;

namespace Domain.Events;

public record FlightInfoChanged(long Id, string FlightId, string Airport, string Airline, DateTime ScheduleTime,
    FlightType Type, string SourceOrDestinationAirport,
    FlightStatus? Status) : INotification;