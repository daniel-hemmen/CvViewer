using CvViewer.DataAccess.Entities;
using CvViewer.Domain;

namespace CvViewer.DataAccess.Mappers;

public static class AdresMapper
{
    public static AdresEntity ToEntity(this Domain.Adres? adres)
    {
        ArgumentNullException.ThrowIfNull(adres);

        return new()
        {
            Straat = adres.Straat,
            Huisnummer = adres.Huisnummer,
            Postcode = adres.Postcode,
            Plaats = adres.Plaats,
            Land = adres.Land
        };
    }

    public static Adres ToDomain(this AdresEntity entity)
        => new()
        {
            Straat = entity.Straat,
            Huisnummer = entity.Huisnummer,
            Postcode = entity.Postcode,
            Plaats = entity.Plaats,
            Land = entity.Land
        };
}
