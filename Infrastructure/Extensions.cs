using Domain;
using Infrastructure.DTOs;

namespace Infrastructure;

internal static class Extensions
{
    public static IReadOnlyCollection<Flight> ToDomainModel(this IReadOnlyCollection<FlightDTO> flights)
    {
        return flights.Select(f => new Flight(f.UniqueId, f.FlightId, f.AirlineCode, f.ScheduleTime, MapFlightType(f.ArrDep),
            f.AirportCode, MapFlightStatus(f.StatusDto))).ToList();
    }

    private static FlightStatus? MapFlightStatus(FlightStatusDTO? flightStatus)
    {
        if (flightStatus?.Code is null)
        {
            return null;
        }

        return flightStatus.Code switch
        {
            "A" => FlightStatus.Arrived,
            "C" => FlightStatus.Canceled,
            "D" => FlightStatus.Departed,
            "E" => FlightStatus.NewTime,
            "N" => FlightStatus.NewInfo
        };
    }

    private static FlightType MapFlightType(string flightTypeCode)
    {
        return flightTypeCode == "A" ? FlightType.Arrival : FlightType.Departure;
    }
}