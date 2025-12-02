using CvViewer.Api.DTOs;
using CvViewer.Domain;

namespace CvViewer.Api.Mappers;

public static class CertificaatInstanceDtoMapper
{
    public static CertificaatInstanceDto ToDto(this CertificaatInstance certificaatInstance)
    => new(
        certificaatInstance.Naam,
        certificaatInstance.Uitgever,
        certificaatInstance.DatumAfgifte.ToString(),
        certificaatInstance.Verloopdatum?.ToString(),
        certificaatInstance.Url);
}
