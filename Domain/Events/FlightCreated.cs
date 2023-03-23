using MediatR;

namespace Domain.Events;

public record FlightCreated(long Id, string FlightId, string Airport, string Airline, DateTime ScheduleTime, FlightType Type, string DestinationOrSourceAirport,
    FlightStatus? Status) : INotification;