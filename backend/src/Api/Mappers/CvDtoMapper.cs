using CvViewer.Api.DTOs;
using CvViewer.Domain;
using CvViewer.Domain.Extensions;

namespace CvViewer.Api.Mappers;

public static class CvDtoMapper
{
    public static CvDto ToDto(this Cv? cv)
    {
        ArgumentNullException.ThrowIfNull(cv);

        return new(
            Id: cv.Id,
            Auteur: cv.Auteur.FullName,
            Telefoonnummer: cv.Contactgegevens?.Telefoonnummer,
            Email: cv.Contactgegevens?.Email,
            Adres: cv.Auteur.Adres?.FullAdres,
            Inleiding: cv.Inleiding,
            WerkervaringInstances: [.. cv.WerkervaringInstances.Select(instance => instance.ToDto())],
            OpleidingInstances: [.. cv.OpleidingInstances.Select(instance => instance.ToDto())],
            CertificaatInstances: [.. cv.CertificaatInstances.Select(instance => instance.ToDto())],
            VaardigheidInstances: [.. cv.VaardigheidInstances.Select(instance => instance.ToDto())],
            IsFavorited: cv.IsFavorited,
            LastUpdated: cv.LastUpdated.ToDateTimeOffset()
        );
    }
}
