namespace CvViewer.Domain;

public sealed record VaardigheidInstance
{
    public required string Naam { get; init; }
    public required byte Niveau { get; init; }
}
