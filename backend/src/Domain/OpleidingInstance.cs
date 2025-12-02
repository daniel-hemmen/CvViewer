using CvViewer.Types;

namespace CvViewer.Domain;

public sealed record OpleidingInstance
{
    public required string Naam { get; init; }
    public required string Instituut { get; init; }
    public required DateParts? Startdatum { get; init; }
    public DateParts Einddatum { get; init; }
    public string? Beschrijving { get; init; }

    public override string ToString() => $"{Instituut} - {Naam} | {new DatePartsRange(Startdatum, Einddatum)}";
}
