using CvViewer.Api.DTOs;
using CvViewer.Domain;

namespace CvViewer.Api.Mappers;

public static class WerkervaringInstanceDtoMapper
{
    public static WerkervaringInstanceDto ToDto(this WerkervaringInstance werkervaringInstance)
        => new(
            werkervaringInstance.Rol,
            werkervaringInstance.Organisatie,
            werkervaringInstance.Startdatum.ToString(),
            werkervaringInstance.Einddatum?.ToString(),
            werkervaringInstance.Beschrijving,
            werkervaringInstance.Plaats);
}
