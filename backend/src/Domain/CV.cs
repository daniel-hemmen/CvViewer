
namespace CvViewer.Domain;

public sealed record Cv
{
    public required Auteur Auteur { get; init; }
    public Contactgegevens? Contactgegevens { get; init; }
    public Adres? Adres { get; init; }
    public string? Inleiding { get; init; }
    public List<WerkervaringInstance> WerkervaringInstances { get; init; } = [];
    public List<OpleidingInstance> OpleidingInstances { get; init; } = [];
    public List<CertificaatInstance> CertificaatInstances { get; init; } = [];
    public List<VaardigheidInstance> VaardigheidInstances { get; init; } = [];
    public CvMetadata? Metadata { get; set; }
}
