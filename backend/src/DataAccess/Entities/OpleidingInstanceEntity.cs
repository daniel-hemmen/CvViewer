namespace CvViewer.DataAccess.Entities;

public class OpleidingInstanceEntity
{
    public long Id { get; set; }
    public required string Naam { get; set; }
    public required string Instituut { get; set; }
    public required DateOnly? Startdatum { get; set; }
    public DateOnly Einddatum { get; set; }
    public string? Beschrijving { get; set; }
}
