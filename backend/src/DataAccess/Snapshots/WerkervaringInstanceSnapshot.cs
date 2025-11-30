


namespace CvViewer.DataAccess.Snapshots;

public class WerkervaringInstanceSnapshot
{
    public required string Bedrijfsnaam { get; init; }
    public required string Functietitel { get; init; }
    public DateOnly Startdatum { get; init; }
    public DateOnly? Einddatum { get; init; }
    public string? Locatie { get; init; }
    public string? Beschrijving { get; init; }
}
