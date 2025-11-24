using CvViewer.ApplicationServices.DTOs;
using CvViewer.Domain;

namespace CvViewer.ApplicationServices.Mappers
{
    public static class CvDtoMapper
    {
        public static CvDto ToDto(this Cv? cv)
        {
            ArgumentNullException.ThrowIfNull(cv);

            return new()
            {
                AuteurNaam = cv.Auteur.Achternaam,
                Email = cv.Contactgegevens?.Email,
                Locatie = cv.Adres?.Straat,
                Inleiding = cv.Inleiding,
            };
        }
    }
}
