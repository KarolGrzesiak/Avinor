
namespace Domain.Events;

public record FlightArrived(string Id, string FlightId, string Airport) : IEvent;