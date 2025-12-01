using System.ComponentModel.DataAnnotations;
using CvViewer.DataAccess.Snapshots;
using NodaTime;

namespace CvViewer.DataAccess.Entities;

public class CvEntity
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }

    public long AuteurId { get; set; }
    public required AuteurEntity Auteur { get; set; }

    public ContactgegevensSnapshot? ContactgegevensSnapshot { get; set; }
    public AdresSnapshot? AdresSnapshot { get; set; }

    public List<WerkervaringInstanceSnapshot> WerkervaringInstances { get; set; } = [];
    public List<OpleidingInstanceSnapshot> OpleidingInstances { get; set; } = [];
    public List<CertificaatInstanceSnapshot> CertificaatInstances { get; set; } = [];
    public List<VaardigheidInstanceSnapshot> VaardigheidInstances { get; set; } = [];

    public string? Inleiding { get; set; }
    public bool IsFavorite { get; set; }
    public Instant LastUpdated { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = default!;
}
