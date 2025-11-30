namespace CvViewer.DataAccess.Entities;

public class WerkervaringInstanceEntity
{
    public long Id { get; set; }
    public required string Rol { get; set; }
    public required string Organisatie { get; set; }
    public required DateOnly Startdatum { get; set; }
    public DateOnly? Einddatum { get; set; }
    public string? Beschrijving { get; set; }
}
