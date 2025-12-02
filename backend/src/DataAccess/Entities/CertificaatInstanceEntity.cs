using CvViewer.Types;

namespace CvViewer.DataAccess.Entities;

public class CertificaatInstanceEntity
{
    public long Id { get; set; }
    public required string Naam { get; set; }
    public required string Uitgever { get; set; }
    public required DateParts DatumAfgifte { get; set; }
    public DateParts? Verloopdatum { get; set; }
    public string? Url { get; set; }
}
