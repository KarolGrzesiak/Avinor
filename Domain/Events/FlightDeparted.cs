
namespace Domain.Events;

public record FlightDeparted(string Id, string FlightId, string Airport) : IEvent;