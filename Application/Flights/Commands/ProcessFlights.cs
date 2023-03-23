using Domain;
using MediatR;

namespace Application.Flights.Commands;

public record ProcessFlights(string Airport, IReadOnlyCollection<Flight> Flights) : IRequest;