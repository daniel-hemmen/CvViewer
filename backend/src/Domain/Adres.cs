namespace CvViewer.Domain;

public sealed record Adres
{
    public required string Straat { get; init; }
    public required string Huisnummer { get; init; }
    public required string Plaats { get; init; }
    public required string Postcode { get; init; }
    public required string Land { get; init; }
}
