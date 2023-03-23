using Application.Flights.Queries.Views;
using MediatR;

namespace Application.Flights.Queries;

public record GetFlights(string Airport) : IRequest<FlightsView>;