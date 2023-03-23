using System.Xml.Serialization;

namespace Infrastructure.DTOs;

public record FlightDTO
{
    [XmlAttribute("uniqueID")] public long UniqueId { get; set; }
    [XmlElement("flight_id")] public string FlightId { get; set; }

    [XmlElement("dom_int")] public string DomInt { get; set; }

    [XmlElement("schedule_time")] public DateTime ScheduleTime { get; set; }

    [XmlElement("arr_dep")] public string ArrDep { get; set; }

    [XmlElement("airport")] public string AirportCode { get; set; }

    [XmlElement("airline")] public string AirlineCode { get; set; }

    [XmlElement("via_airport")] public string ViaAirport { get; set; }

    [XmlElement("check_in")] public string CheckIn { get; set; }

    [XmlElement("gate")] public string Gate { get; set; }

    [XmlElement("status")] public FlightStatusDTO StatusDto { get; set; }
}