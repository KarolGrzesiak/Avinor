using Infrastructure.DTOs;

namespace Infrastructure.Clients;

internal interface IAvinorClient
{
    Task<IReadOnlyCollection<FlightDTO>> GetFlightsAsync(string airportCode, DateTime after, CancellationToken cancellationToken);
}