using Domain.Entities;
using Domain.Repositories;
using Marten;

namespace Infrastructure.Repositories;

internal class FlightsRepository : IFlightsRepository
{
    private readonly IDocumentSession _session;

    public FlightsRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Flight?> LoadAsync(string id, int? version = null, CancellationToken cancellationToken = default)
    {
        var aggregate = await _session.Events.AggregateStreamAsync<Flight>(id, version ?? 0,null,null,0, cancellationToken);
        return aggregate;
    }
}