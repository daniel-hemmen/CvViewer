using CvViewer.Types;

namespace CvViewer.DataAccess.Snapshots;

public class OpleidingInstanceSnapshot
{
    public required string Naam { get; set; }
    public required string Instituut { get; set; }
    public DateParts? Startdatum { get; set; }
    public DateParts Einddatum { get; set; }
    public string? Beschrijving { get; set; }
}
