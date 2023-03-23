using Application.Flights.Commands;
using Infrastructure.Clients;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

internal class FlightsWorker : BackgroundService
{
    private readonly IAvinorClient _avinorClient;
    private readonly IServiceProvider _serviceProvider;

    private readonly PeriodicTimer _timer;
    private readonly IReadOnlyCollection<AirportMetadata> _airports;

    public FlightsWorker(IOptions<List<AirportMetadata>> airportsOptions, IAvinorClient avinorClient,
        IServiceProvider serviceProvider)
    {
        _avinorClient = avinorClient;
        _serviceProvider = serviceProvider;
        _airports = airportsOptions.Value;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(3));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var lastCall = DateTime.UtcNow;
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            var newCall = DateTime.UtcNow;
            foreach (var airport in _airports)
            {
                var result = await _avinorClient.GetFlightsAsync(airport.Code, lastCall, stoppingToken);
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new ProcessFlights(airport.Code, result.ToDomainModel()), stoppingToken);
            }


            lastCall = newCall;
        }
    }
}