namespace CvViewer.ApplicationServices.DTOs;

public sealed record CvDto()
{
    public Guid Id { get; set; }
    public string? Auteur { get; set; }
    public string? Email { get; set; }
    public string? Adres { get; set; }
    public string? Inleiding { get; set; }
    public List<string> WerkervaringInstances { get; set; } = [];
    public List<string> OpleidingInstances { get; set; } = [];
    public List<string> CertificaatInstances { get; set; } = [];
    public List<VaardigheidInstanceDto> VaardigheidInstances { get; set; } = [];
    public bool IsFavorite { get; set; }
    public DateTimeOffset? LastUpdated { get; set; }
}
