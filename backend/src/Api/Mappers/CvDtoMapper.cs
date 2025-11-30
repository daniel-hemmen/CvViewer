using CvViewer.ApplicationServices.DTOs;
using CvViewer.Domain;
using CvViewer.Domain.Extensions;

namespace CvViewer.Api.Mappers;

public static class CvDtoMapper
{
    public static CvDto ToDto(this Cv? cv)
    {
        ArgumentNullException.ThrowIfNull(cv);

        return new()
        {
            Id = cv.Id,
            Auteur = cv.Auteur.FullName,
            Email = cv.Contactgegevens?.Email,
            Adres = cv.Auteur.Adres?.FullAdres,
            Inleiding = cv.Inleiding,
            WerkervaringInstances = [.. cv.WerkervaringInstances.Select(w => w.ToString())],
            OpleidingInstances = [.. cv.OpleidingInstances.Select(o => o.ToString())],
            CertificaatInstances = [.. cv.CertificaatInstances.Select(c => c.ToString())],
            VaardigheidInstances = [.. cv.VaardigheidInstances.Select(v => new VaardigheidInstanceDto(v.Naam, v.Niveau))],
            IsFavorite = cv.IsFavorited,
            LastUpdated = cv.LastUpdated.ToDateTimeOffset()
        };
    }
}
