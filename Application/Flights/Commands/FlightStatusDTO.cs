using System.Xml.Serialization;

namespace Application.Flights.Commands;

public record FlightStatusDTO
{
    [XmlAttribute("code")]
    public string Code { get; set; }

    [XmlAttribute("time")]
    public DateTime Time { get; set; }
}