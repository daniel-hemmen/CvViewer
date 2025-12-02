using CvViewer.Types;

namespace CvViewer.DataAccess.Entities;

public class OpleidingInstanceEntity
{
    public long Id { get; set; }
    public required string Naam { get; set; }
    public required string Instituut { get; set; }
    public required DateParts? Startdatum { get; set; }
    public DateParts Einddatum { get; set; }
    public string? Beschrijving { get; set; }
}
