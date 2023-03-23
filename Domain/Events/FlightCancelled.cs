
namespace Domain.Events;

public record FlightCancelled(string Id, string FlightId, string Airport) : IEvent;