using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Snapshots;
using CvViewer.Domain;

namespace CvViewer.DataAccess.Mappers;

public static class CvMapper
{
    public static CvEntity ToEntity(this Cv cv)
        => new()
        {
            ExternalId = cv.Id,
            Auteur = cv.Auteur.ToEntity(),
            ContactgegevensSnapshot = cv.Contactgegevens?.ToSnapshot(),
            AdresSnapshot = cv.Adres?.ToSnapshot(),
            WerkervaringInstances = [.. cv.WerkervaringInstances.Select(w => w.ToSnapshot())],
            OpleidingInstances = [.. cv.OpleidingInstances.Select(o => o.ToSnapshot())],
            CertificaatInstances = [.. cv.CertificaatInstances.Select(c => c.ToSnapshot())],
            VaardigheidInstances = [.. cv.VaardigheidInstances.Select(v => v.ToSnapshot())],
            Inleiding = cv.Inleiding,
            IsFavorite = cv.IsFavorited,
            LastUpdated = cv.LastUpdated
        };

    public static Cv ToDomain(this CvEntity entity)
        => new()
        {
            Id = entity.ExternalId,
            Auteur = entity.Auteur.ToDomain(),
            Contactgegevens = entity.ContactgegevensSnapshot?.ToDomain(),
            Adres = entity.AdresSnapshot?.ToDomain(),
            WerkervaringInstances = [.. entity.WerkervaringInstances.Select(w => w.ToDomain())],
            OpleidingInstances = [.. entity.OpleidingInstances.Select(o => o.ToDomain())],
            CertificaatInstances = [.. entity.CertificaatInstances.Select(c => c.ToDomain())],
            VaardigheidInstances = [.. entity.VaardigheidInstances.Select(v => v.ToDomain())],
            Inleiding = entity.Inleiding,
            IsFavorited = entity.IsFavorite,
            LastUpdated = entity.LastUpdated
        };

    private static ContactgegevensSnapshot? ToSnapshot(this Contactgegevens? contactgegevens)
        => contactgegevens == null
            ? null
            : new ContactgegevensSnapshot
            {
                Email = contactgegevens.Email,
                Telefoonnummer = contactgegevens.Telefoonnummer,
                LinkedInUrl = contactgegevens.LinkedInUrl
            };

    private static AdresSnapshot? ToSnapshot(this Adres? adres)
        => adres == null
            ? null
            : new AdresSnapshot
            {
                Straat = adres.Straat,
                Huisnummer = adres.Huisnummer,
                Postcode = adres.Postcode,
                Stad = adres.Plaats,
                Land = adres.Land
            };

    private static WerkervaringInstanceSnapshot ToSnapshot(this WerkervaringInstance werkervaring)
        => new()
        {
            Bedrijfsnaam = werkervaring.Bedrijfsnaam,
            Functietitel = werkervaring.Functietitel,
            Startdatum = werkervaring.Startdatum,
            Einddatum = werkervaring.Einddatum,
            Locatie = werkervaring.Locatie,
            Beschrijving = werkervaring.Beschrijving
        };

    private static OpleidingInstanceSnapshot ToSnapshot(this OpleidingInstance opleiding)
        => new()
        {
            Title = opleiding.Naam,
            Instituut = opleiding.Instituut,
            Startdatum = opleiding.Startdatum,
            Einddatum = opleiding.Einddatum,
            Beschrijving = opleiding.Beschrijving
        };

    private static CertificaatInstanceSnapshot ToSnapshot(this CertificaatInstance certificaat)
        => new()
        {
            Naam = certificaat.Naam,
            Uitgever = certificaat.Uitgever,
            DatumAfgifte = certificaat.DatumAfgifte,
            Verloopdatum = certificaat.Verloopdatum,
            Url = certificaat.Url
        };

    private static VaardigheidInstanceSnapshot ToSnapshot(this VaardigheidInstance vaardigheid)
        => new()
        {
            Naam = vaardigheid.Naam,
            Niveau = vaardigheid.Niveau
        };

    private static Contactgegevens? ToDomain(this ContactgegevensSnapshot? snapshot)
        => snapshot == null
            ? null
            : new Contactgegevens
            {
                Email = snapshot.Email,
                Telefoonnummer = snapshot.Telefoonnummer,
                LinkedInUrl = snapshot.LinkedInUrl
            };

    private static Adres? ToDomain(this AdresSnapshot? snapshot)
        => snapshot == null
            ? null
            : new Adres
            {
                Straat = snapshot.Straat ?? "",
                Huisnummer = snapshot.Huisnummer ?? "",
                Postcode = snapshot.Postcode ?? "",
                Plaats = snapshot.Stad ?? "",
                Land = snapshot.Land ?? ""
            };

    private static WerkervaringInstance ToDomain(this WerkervaringInstanceSnapshot snapshot)
        => new()
        {
            Bedrijfsnaam = snapshot.Bedrijfsnaam,
            Functietitel = snapshot.Functietitel,
            Startdatum = snapshot.Startdatum,
            Einddatum = snapshot.Einddatum,
            Locatie = snapshot.Locatie,
            Beschrijving = snapshot.Beschrijving
        };

    private static OpleidingInstance ToDomain(this OpleidingInstanceSnapshot snapshot)
        => new()
        {
            Naam = snapshot.Title ?? "",
            Instituut = snapshot.Instituut ?? "",
            Startdatum = snapshot.Startdatum,
            Einddatum = snapshot.Einddatum ?? DateOnly.MinValue,
            Beschrijving = snapshot.Beschrijving
        };

    private static CertificaatInstance ToDomain(this CertificaatInstanceSnapshot snapshot)
        => new()
        {
            Naam = snapshot.Naam ?? "",
            Uitgever = snapshot.Uitgever ?? "",
            DatumAfgifte = snapshot.DatumAfgifte ?? DateOnly.MinValue,
            Verloopdatum = snapshot.Verloopdatum,
            Url = snapshot.Url
        };

    private static VaardigheidInstance ToDomain(this VaardigheidInstanceSnapshot snapshot)
        => new()
        {
            Naam = snapshot.Naam ?? "",
            Niveau = snapshot.Niveau ?? 0
        };
}
