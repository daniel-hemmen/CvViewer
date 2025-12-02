using CvViewer.Types;

namespace CvViewer.DataAccess.Snapshots;

public class WerkervaringInstanceSnapshot
{
    public required string Rol { get; init; }
    public required string Organisatie { get; init; }
    public DateParts Startdatum { get; init; }
    public DateParts? Einddatum { get; init; }
    public string? Beschrijving { get; init; }
    public string? Plaats { get; init; }
}
