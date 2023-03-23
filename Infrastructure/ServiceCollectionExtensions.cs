using Application.Common;
using Application.Flights.Commands;
using Application.Flights.Queries;
using Domain.Repositories;
using Infrastructure.Clients;
using Infrastructure.Projections.Flights;
using Infrastructure.Queries.Flights;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Marten;
using Marten.Events;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Weasel.Core;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<List<AirportMetadata>>()
            .Bind(configuration.GetSection("Airports"));
        services.AddHostedService<FlightsWorker>();
        services.AddHttpClient<IAvinorClient, AvinorClient>(
                (_, httpClient) =>
                {
                    httpClient.BaseAddress = new Uri("https://flydata.avinor.no/XmlFeed.asp?TimeFrom=1&TimeTo=24");
                })
            .AddPolicyHandler(GetRetryPolicy());
        services.AddScoped<IFlightsRepository, FlightsRepository>();
        services.AddScoped<IEventProcessor, EventProcessor>();
        services.AddMarten(options =>
        {
            options.AutoCreateSchemaObjects = AutoCreate.All;
            options.UseDefaultSerialization(EnumStorage.AsString);
            options.Connection(configuration.GetConnectionString("Flights")!);
            options.Events.StreamIdentity = StreamIdentity.AsString;
            options.Projections.Add<FlightsViewProjection>(ProjectionLifecycle.Async);
            options.Schema.For<FlightsView>().Identity(f => f.Airport);
        }).UseLightweightSessions().AddAsyncDaemon(DaemonMode.Solo);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining(typeof(ProcessFlightsHandler))
                .RegisterServicesFromAssemblyContaining(typeof(GetFlightsHandler)));
        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(delay);
    }
}