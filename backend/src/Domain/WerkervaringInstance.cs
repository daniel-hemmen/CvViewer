using CvViewer.Types;

namespace CvViewer.Domain;

public sealed record WerkervaringInstance
{
    public required string Rol { get; init; }
    public required string Organisatie { get; init; }
    public DateParts Startdatum { get; init; }
    public DateParts? Einddatum { get; init; }
    public string? Beschrijving { get; init; }
    public string? Plaats { get; init; }

    public override string ToString() => $"{Organisatie} - {Rol} | {new DatePartsRange(Startdatum, Einddatum)}";
}
