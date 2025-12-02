using CvViewer.Api.DTOs;
using CvViewer.Domain;

namespace CvViewer.Api.Mappers;

public static class OpleidingInstanceDtoMapper
{
    public static OpleidingInstanceDto ToDto(this OpleidingInstance opleidingInstance)
        => new(
            opleidingInstance.Naam,
            opleidingInstance.Instituut,
            opleidingInstance.Startdatum?.ToString(),
            opleidingInstance.Einddatum.ToString(),
            opleidingInstance.Beschrijving);
}
