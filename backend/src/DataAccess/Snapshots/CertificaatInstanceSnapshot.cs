using CvViewer.Types;

namespace CvViewer.DataAccess.Snapshots;

public class CertificaatInstanceSnapshot
{
    public required string Naam { get; set; }
    public required string Uitgever { get; set; }
    public DateParts DatumAfgifte { get; set; }
    public DateParts? Verloopdatum { get; set; }
    public string? Url { get; set; }
}
