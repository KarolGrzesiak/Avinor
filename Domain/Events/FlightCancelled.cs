using MediatR;

namespace Domain.Events;

public record FlightCancelled(long Id, string FlightId, string Airport) : INotification;