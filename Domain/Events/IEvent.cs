namespace Domain.Events;

public interface IEvent
{
    public string Id { get; init; }
}