using MediatR;

namespace Domain.Events;

public record FlightArrived(long Id, string FlightId, string Airport) : INotification;