namespace Application.Flights.Queries;

public record FlightsView
{
    public string Airport { get; set; }
    public IList<FlightView> Flights { get; set; } = new List<FlightView>();
}