namespace Infrastructure;

internal record AirportMetadata
{
    public string Name { get; set; }

    private string _code;
    public string Code
    {
        get => _code;
        set => _code = value.ToUpper();
    }

    public string Type { get; set; }
}