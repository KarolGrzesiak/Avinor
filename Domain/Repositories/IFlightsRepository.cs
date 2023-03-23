using Domain.Entities;

namespace Domain.Repositories;

public interface IFlightsRepository
{
    Task<Flight?> LoadAsync(string id, int? version = null, CancellationToken cancellationToken = default);
}