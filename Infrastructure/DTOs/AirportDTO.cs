using System.Xml.Serialization;

namespace Infrastructure.DTOs;

public record AirportDTO
{
    [XmlArray("flights")]
    [XmlArrayItem("flight", typeof(FlightDTO))]
    public List<FlightDTO> Flights { get; set; }

    [XmlAttribute("lastUpdate")]
    public DateTime LastUpdate { get; set; }
}