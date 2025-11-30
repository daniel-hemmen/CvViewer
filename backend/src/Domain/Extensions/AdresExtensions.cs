namespace CvViewer.Domain.Extensions;

public static class AdresExtensions
{
    extension(Adres adres)
    {
        public string FullAdres => $"{adres.Straat} {adres.Huisnummer}, {adres.Postcode} {adres.Plaats}";
    }
}
