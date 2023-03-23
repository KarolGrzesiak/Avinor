using Domain.Events;

namespace Domain.Entities;

public class Flight : AggregateBase
{
    private Flight()
    {
    }

    public Flight(string id, string flightId, string airport, string airline, DateTime scheduleTime, FlightType type,
        string sourceOrDestinationAirport,
        FlightStatus? status)
    {
        var @event = new FlightCreated(id, flightId, airport, airline, scheduleTime, type,
            sourceOrDestinationAirport, status);
        Apply(@event);
        AddUncommittedEvent(@event);
    }

    public string Airport { get; private set; }
    public string FlightId { get; private set; }
    public string Airline { get; private set; }
    public DateTime ScheduleTime { get; private set; }
    public FlightType Type { get; private set; }
    public string SourceOrDestinationAirport { get; private set; }
    public FlightStatus? Status { get; private set; }

    public void Cancel()
    {
        var @event = new FlightCancelled(Id, FlightId, Airport);
        Apply(@event);
        AddUncommittedEvent(@event);
    }

    private void Apply(FlightCreated @event)
    {
        Id = @event.Id;
        FlightId = @event.FlightId;
        Airport = @event.Airport;
        Airline = @event.Airline;
        ScheduleTime = @event.ScheduleTime;
        Type = @event.Type;
        SourceOrDestinationAirport = @event.DestinationOrSourceAirport;
        Status = @event.Status;
        Version++;
    }

    private void Apply(FlightCancelled @event)
    {
        Status = FlightStatus.Canceled;
        Version++;
    }

    public void SetAsDeparted()
    {
        var @event = new FlightDeparted(Id, FlightId, Airport);
        Apply(@event);
        AddUncommittedEvent(@event);
    }

    private void Apply(FlightDeparted @event)
    {
        Status = FlightStatus.Departed;
        Version++;
    }

    public void SetAsArrived()
    {
        var @event = new FlightArrived(Id, FlightId, Airport);
        Apply(@event);
        AddUncommittedEvent(@event);
    }

    private void Apply(FlightArrived @event)
    {
        Status = FlightStatus.Arrived;
        Version++;
    }

    public void ChangeInfo(string flightId, string airport, string airline, DateTime scheduleTime, FlightType type,
        FlightStatus? status, string sourceOrDestinationAirport)
    {
        var @event = new FlightInfoChanged(Id, flightId, airport,
            airline, scheduleTime,
            type, sourceOrDestinationAirport, status);
        Apply(@event);
        AddUncommittedEvent(@event);
    }

    private void Apply(FlightInfoChanged @event)
    {
        FlightId = @event.FlightId;
        Airport = @event.Airport;
        Airline = @event.Airline;
        ScheduleTime = @event.ScheduleTime;
        Type = @event.Type;
        Status = @event.Status;
        SourceOrDestinationAirport = @event.SourceOrDestinationAirport;
        Version++;
    }

    public void ChangeTime(DateTime scheduleTime)
    {
        var @event = new FlightTimeChanged(Id, FlightId, Airport,
            scheduleTime);
        Apply(@event);
        AddUncommittedEvent(@event);
    }

    private void Apply(FlightTimeChanged @event)
    {
        ScheduleTime = @event.ScheduleTime;
        Version++;
    }
}