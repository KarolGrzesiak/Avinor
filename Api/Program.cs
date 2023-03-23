using System.Text.Json.Serialization;
using Application.Flights.Queries;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/flights/{airport}", async (string airport, IMediator mediator) =>
    await mediator.Send(new GetFlights(airport.ToUpper())));
app.Run();