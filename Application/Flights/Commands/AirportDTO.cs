using System.Xml.Serialization;

namespace Application.Flights.Commands;

public record AirportDTO
{
    [XmlArray("flights")]
    [XmlArrayItem("flight", typeof(FlightDTO))]
    public List<FlightDTO> Flights { get; set; }

    [XmlAttribute("lastUpdate")]
    public DateTime LastUpdate { get; set; }
}