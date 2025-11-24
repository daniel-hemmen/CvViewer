using CvViewer.DataAccess.Entities;
using CvViewer.Domain;

namespace CvViewer.DataAccess.Mappers
{
    public static class AuteurMapper
    {
        public static AuteurEntity ToEntity(this Auteur auteur)
            => new()
            {
                Voornaam = auteur.Voornaam,
                Tussenvoegsel = auteur.Tussenvoegsel,
                Achternaam = auteur.Achternaam,
                Adres = auteur.Adres.ToEntity(),
                Contactgegevens = auteur.Contactgegevens.ToEntity(),
            };
    }
}
