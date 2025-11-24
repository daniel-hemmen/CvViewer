namespace CvViewer.DataAccess.Entities;

public class CertificaatInstanceEntity
{
    public long Id { get; set; }
    public required string Naam { get; set; }
    public required string Uitgever { get; set; }
    public required DateOnly DatumAfgifte { get; set; }
    public DateOnly? Verloopdatum { get; set; }
    public string? Url { get; set; }
}
