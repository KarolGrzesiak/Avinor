namespace Application.Flights.Queries.Views;

public record FlightsView
{
    public string Airport { get; set; }
    public IList<FlightView> Flights { get; set; } = new List<FlightView>();
}