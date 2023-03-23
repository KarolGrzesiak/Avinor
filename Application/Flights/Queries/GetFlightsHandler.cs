using Application.Flights.Queries.Views;
using Marten;
using MediatR;

namespace Application.Flights.Queries;

public class GetFlightsHandler : IRequestHandler<GetFlights, FlightsView>
{
    private readonly IQuerySession _session;

    public GetFlightsHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<FlightsView> Handle(GetFlights request, CancellationToken cancellationToken)
    {
        var result = await _session.Query<FlightsView>()
            .FirstOrDefaultAsync(x => x.Airport == request.Airport, token: cancellationToken);
        return result ?? new FlightsView { Airport = request.Airport };
    }
}