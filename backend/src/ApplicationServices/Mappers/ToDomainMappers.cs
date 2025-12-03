using CvViewer.ApplicationServices.FileReader.DTOs;
using CvViewer.Domain;
using CvViewer.Types;

namespace CvViewer.ApplicationServices.Mappers;

public static class ToDomainMappers
{
    public static Adres ToDomain(this AdresDto adresDto)
        => new()
        {
            Straat = adresDto.Straat,
            Huisnummer = adresDto.Huisnummer,
            Plaats = adresDto.Plaats,
            Postcode = adresDto.Postcode,
            Land = adresDto.Land
        };

    public static Auteur ToDomain(this AuteurDto auteurDto, Adres? adres, Contactgegevens? contactgegevens)
        => new()
        {
            Voornaam = auteurDto.Voornaam,
            Tussenvoegsel = auteurDto.Tussenvoegsel,
            Achternaam = auteurDto.Achternaam,
            Adres = adres,
            Contactgegevens = contactgegevens
        };

    public static Contactgegevens ToDomain(this ContactgegevensDto contactgegevensDto)
        => new()
        {
            Email = contactgegevensDto.Email,
            Telefoonnummer = contactgegevensDto.Telefoonnummer,
            LinkedInUrl = contactgegevensDto.LinkedinUrl
        };

    public static CertificaatInstance ToDomain(this CertificaatDto certificaatDto)
        => new()
        {
            Naam = certificaatDto.Naam,
            Uitgever = certificaatDto.Uitgever,
            DatumAfgifte = ToDateParts(
                certificaatDto.DatumAfgifteDag,
                certificaatDto.DatumAfgifteMaand,
                certificaatDto.DatumAfgifteJaar),
            Verloopdatum = ToNullableDateParts(
                certificaatDto.VerloopdatumDag,
                certificaatDto.VerloopdatumMaand,
                certificaatDto.VerloopdatumJaar),
            Url = certificaatDto.Url
        };

    public static OpleidingInstance ToDomain(this OpleidingDto opleidingDto)
        => new()
        {
            Naam = opleidingDto.Naam,
            Instituut = opleidingDto.Instituut,
            Startdatum = ToNullableDateParts(
                opleidingDto.StartdatumDag,
                opleidingDto.StartdatumMaand,
                opleidingDto.StartdatumJaar),
            Einddatum = ToDateParts(
                opleidingDto.EinddatumDag,
                opleidingDto.EinddatumMaand,
                opleidingDto.EinddatumJaar),
            Beschrijving = opleidingDto.Beschrijving
        };

    public static VaardigheidInstance ToDomain(this VaardigheidDto vaardigheidDto)
        => new()
        {
            Naam = vaardigheidDto.Naam,
            Niveau = (byte)vaardigheidDto.Niveau
        };

    public static WerkervaringInstance ToDomain(this WerkervaringDto werkervaringDto)
        => new()
        {
            Rol = werkervaringDto.Rol,
            Organisatie = werkervaringDto.Organisatie,
            Startdatum = ToDateParts(
                werkervaringDto.StartdatumDag,
                werkervaringDto.StartdatumMaand,
                werkervaringDto.StartdatumJaar),
            Einddatum = ToNullableDateParts(werkervaringDto.EinddatumDag,
                          werkervaringDto.EinddatumMaand,
                          werkervaringDto.EinddatumJaar),
            Beschrijving = werkervaringDto.Beschrijving,
            Plaats = werkervaringDto.Plaats
        };

    private static DateParts? ToNullableDateParts(int? day, int? month, int? year)
    {
        if (day is null && month is null && year is null)
        {
            return null;
        }

        return new DateParts(year!.Value, month, day);
    }

    private static DateParts ToDateParts(int? day, int? month, int year)
        => new(year, month, day);
}
