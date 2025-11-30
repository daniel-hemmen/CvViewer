using CvViewer.DataAccess.Entities;
using CvViewer.Domain;

namespace CvViewer.DataAccess.Mappers;

public static class AuteurMapper
{
    public static AuteurEntity ToEntity(this Auteur auteur)
        => new()
        {
            ExternalId = Guid.NewGuid(),
            Voornaam = auteur.Voornaam,
            Tussenvoegsel = auteur.Tussenvoegsel,
            Achternaam = auteur.Achternaam,
            Adres = auteur.Adres.ToEntity(),
            Contactgegevens = auteur.Contactgegevens.ToEntity(),
        };

    public static Auteur ToDomain(this AuteurEntity entity)
        => new()
        {
            Voornaam = entity.Voornaam,
            Tussenvoegsel = entity.Tussenvoegsel,
            Achternaam = entity.Achternaam,
            Adres = entity.Adres.ToDomain(),
            Contactgegevens = entity.Contactgegevens.ToDomain()
        };
}
