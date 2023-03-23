using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Domain.Events;

namespace Domain.Entities;

public abstract class AggregateBase
{
    public string Id { get; protected set; }

    public long Version { get; set; }

    [JsonIgnore] private readonly List<IEvent> _uncommittedEvents = new();

    public IReadOnlyCollection<IEvent> GetUncommittedEvents()
    {
        return _uncommittedEvents.ToImmutableList();
    }

    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }

    protected void AddUncommittedEvent(IEvent @event)
    {
        _uncommittedEvents.Add(@event);
    }
}