using System.Xml;
using System.Xml.Serialization;
using Application;
using Application.Flights.Commands;
using Flurl;

namespace Infrastructure.Clients;

internal class AvinorClient : IAvinorClient
{
    private readonly HttpClient _httpClient;
    private readonly XmlSerializer _serializer;
    private const string Iso8601DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";

    public AvinorClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializer = new XmlSerializer(typeof(AirportDTO), new XmlRootAttribute("airport"));
    }

    public async Task<IReadOnlyCollection<FlightDTO>> GetFlightsAsync(string airportCode, DateTime after,
        CancellationToken cancellationToken)
    {
        var url = _httpClient.BaseAddress?.ToString().SetQueryParams(new
            { airport = airportCode, lastUpdate = after.AddDays(-1).ToString(Iso8601DateTimeFormat) });
        var response =
            await _httpClient.GetAsync(url,
                cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Array.Empty<FlightDTO>();
        }

        var reader = XmlReader.Create(await response.Content.ReadAsStreamAsync(cancellationToken),
            new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore });
        var result = (AirportDTO)_serializer.Deserialize(reader)!;
        return result.Flights;
    }
}