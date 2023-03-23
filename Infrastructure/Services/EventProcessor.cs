using Application.Common;
using Domain.Entities;
using Domain.Events;
using Marten;

namespace Infrastructure.Services;

internal class EventProcessor :
    IEventProcessor
{
    private readonly IDocumentSession _session;

    public EventProcessor(IDocumentSession session)
    {
        _session = session;
    }

    public async Task ProcessAsync(AggregateBase aggregate, CancellationToken cancellationToken)
    {
        var events = aggregate.GetUncommittedEvents().ToArray();
        _session.Events.Append(aggregate.Id, aggregate.Version, events);

        await _session.SaveChangesAsync(cancellationToken);
    }
}