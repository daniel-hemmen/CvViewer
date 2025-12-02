namespace CvViewer.Api.DTOs;

public sealed record CvDto(
    Guid Id,
    string? Auteur,
    string? Email,
    string? Adres,
    string? Inleiding,
    List<WerkervaringInstanceDto> WerkervaringInstances,
    List<OpleidingInstanceDto> OpleidingInstances,
    List<CertificaatInstanceDto> CertificaatInstances,
    List<VaardigheidInstanceDto> VaardigheidInstances,
    bool IsFavorited,
    DateTimeOffset? LastUpdated);
