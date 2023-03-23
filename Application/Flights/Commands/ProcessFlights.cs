using MediatR;

namespace Application.Flights.Commands;

public record ProcessFlights(string Airport, IReadOnlyCollection<FlightDTO> Flights) : IRequest;