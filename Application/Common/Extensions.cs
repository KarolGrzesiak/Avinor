using Application.Flights.Commands;
using Domain;
using Domain.Entities;

namespace Application.Common;

internal static class Extensions
{
    public static FlightStatus? MapFlightStatus(this FlightStatusDTO? flightStatus)
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

    public static FlightType MapFlightType(this string flightTypeCode)
    {
        return flightTypeCode == "A" ? FlightType.Arrival : FlightType.Departure;
    }
}