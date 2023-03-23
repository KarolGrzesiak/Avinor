using MediatR;

namespace Domain.Events;

public record FlightDeparted(long Id, string FlightId, string Airport) : INotification;