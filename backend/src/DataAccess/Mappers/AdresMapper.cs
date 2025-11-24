using CvViewer.DataAccess.Entities;

namespace CvViewer.DataAccess.Mappers
{
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
    }
}
