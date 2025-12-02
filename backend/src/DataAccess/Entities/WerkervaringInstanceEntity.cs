using CvViewer.Types;

namespace CvViewer.DataAccess.Entities;

public class WerkervaringInstanceEntity
{
    public long Id { get; set; }
    public required string Rol { get; set; }
    public required string Organisatie { get; set; }
    public required DateParts Startdatum { get; set; }
    public DateParts? Einddatum { get; set; }
    public string? Beschrijving { get; set; }
    public string? Plaats { get; set; }
}
