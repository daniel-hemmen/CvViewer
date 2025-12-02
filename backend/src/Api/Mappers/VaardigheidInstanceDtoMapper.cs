using CvViewer.Api.DTOs;
using CvViewer.Domain;

namespace CvViewer.Api.Mappers;

public static class VaardigheidInstanceDtoMapper
{
    public static VaardigheidInstanceDto ToDto(this VaardigheidInstance vaardigheidInstance)
        => new(
            vaardigheidInstance.Naam,
            vaardigheidInstance.Niveau);
}
