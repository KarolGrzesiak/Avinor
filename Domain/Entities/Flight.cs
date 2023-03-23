namespace Domain;

public class Flight
{
    public Flight(long id, string flightId, string airline, DateTime scheduleTime, FlightType type, string sourceOrDestinationAirport,
        FlightStatus? status)
    {
        Id = id;
        FlightId = flightId;
        Airline = airline;
        ScheduleTime = scheduleTime;
        Type = type;
        SourceOrDestinationAirport = sourceOrDestinationAirport;
        Status = status;
    }

    public long Id { get; init; }
    public string FlightId { get; init; }
    public string Airline { get; init; }
    public DateTime ScheduleTime { get; init; }
    public FlightType Type { get; init; }
    public string SourceOrDestinationAirport { get; init; }
    public FlightStatus? Status { get; init; }
}