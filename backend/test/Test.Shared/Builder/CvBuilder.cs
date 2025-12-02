using CvViewer.Domain;
using NodaTime;
using CvViewer.Types;

namespace CvViewer.Test.Shared.Builder;

public sealed class CvBuilder
{
    private Auteur _auteur = new()
    {
        Voornaam = "John",
        Achternaam = "Doe",
        Adres = null,
        Contactgegevens = null
    };

    private Contactgegevens? _contactgegevens;
    private Adres? _adres;
    private string? _inleiding;
    private List<WerkervaringInstance> _werkervaring = [];
    private List<OpleidingInstance> _opleidingInstances = [];
    private List<CertificaatInstance> _certificaatInstances = [];
    private List<VaardigheidInstance> _vaardigheidInstances = [];
    private Instant _lastUpdated;
    private bool _isFavorited = false;

    public static CvBuilder New() => new();

    public static CvBuilder CreateDefault()
        => New()
            .WithAuteur(new Auteur
            {
                Voornaam = "Anna",
                Achternaam = "Jansen",
                Contactgegevens = new Contactgegevens
                {
                    Email = "anna.jansen@example.com",
                    Telefoonnummer = "+31 6 12345678",
                    LinkedInUrl = "https://www.linkedin.com/in/annajansen"
                },
                Adres = new Adres
                {
                    Straat = "Kerkstraat",
                    Huisnummer = "10A",
                    Plaats = "Utrecht",
                    Postcode = "1234AB",
                    Land = "Nederland"
                }
            })
            .WithInleiding("Gedreven software engineer met ervaring in .NET en cloud")
            .AddWerkervaringInstances(new WerkervaringInstance
            {
                Organisatie = "Contoso",
                Rol = "Senior Developer",
                Startdatum = new DateParts(DateTime.UtcNow.Year - 3, DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                Einddatum = null,
                Plaats = "Amsterdam",
                Beschrijving = "Leiding over backend team en architectuur"
            })
            .AddOpleidingInstances(new OpleidingInstance
            {
                Naam = "Computer Science",
                Instituut = "Universiteit van Amsterdam",
                Startdatum = new DateParts(DateTime.UtcNow.Year - 10, DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                Einddatum = new DateParts(DateTime.UtcNow.Year - 6, DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                Beschrijving = "Bachelor of Science"
            })
            .AddCertificaatInstances(new CertificaatInstance
            {
                Naam = "Azure Developer Associate",
                Uitgever = "Microsoft",
                DatumAfgifte = new DateParts(DateTime.UtcNow.Year - 1, DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                Verloopdatum = null,
                Url = "https://learn.microsoft.com"
            })
            .AddVaardigheidInstances([new() { Naam = "C#", Niveau = 5 }]);

    public CvBuilder WithAuteur(Auteur auteur)
    {
        _auteur = auteur;

        return this;
    }

    public CvBuilder WithContactgegevens(Contactgegevens contactgegevens)
    {
        _contactgegevens = contactgegevens;

        return this;
    }

    public CvBuilder WithAdres(Adres adres)
    {
        _adres = adres;

        return this;
    }

    public CvBuilder WithInleiding(string? inleiding)
    {
        _inleiding = inleiding;

        return this;
    }

    public CvBuilder WithWerkervaringInstances(params WerkervaringInstance[] werkervaringInstances)
    {
        _werkervaring = [.. werkervaringInstances];

        return this;
    }

    public CvBuilder AddWerkervaringInstances(params WerkervaringInstance[] werkervaringInstances)
    {
        _werkervaring.AddRange(werkervaringInstances);

        return this;
    }

    public CvBuilder WithOpleidingInstances(params OpleidingInstance[] opleidingInstances)
    {
        _opleidingInstances = [.. opleidingInstances];

        return this;
    }

    public CvBuilder AddOpleidingInstances(params OpleidingInstance[] opleidingInstances)
    {
        _opleidingInstances.AddRange(opleidingInstances);

        return this;
    }

    public CvBuilder WithCertificaatInstances(params CertificaatInstance[] certificaatInstances)
    {
        _certificaatInstances = [.. certificaatInstances];

        return this;
    }

    public CvBuilder AddCertificaatInstances(params CertificaatInstance[] certificaatInstances)
    {
        _certificaatInstances.AddRange(certificaatInstances);

        return this;
    }

    public CvBuilder WithVaardigheidInstances(params VaardigheidInstance[] vaardigheidInstances)
    {
        _vaardigheidInstances = [.. vaardigheidInstances];

        return this;
    }

    public CvBuilder AddVaardigheidInstances(params VaardigheidInstance[] certificaatInstances)
    {
        _vaardigheidInstances.AddRange(certificaatInstances);

        return this;
    }

    public CvBuilder WithLastUpdated(Instant lastUpdated)
    {
        _lastUpdated = lastUpdated;

        return this;
    }

    public CvBuilder WithIsFavorited(bool isFavorited)
    {
        _isFavorited = isFavorited;

        return this;
    }

    public Cv Build()
        => new()
        {
            Id = Guid.NewGuid(),
            Auteur = _auteur,
            Contactgegevens = _contactgegevens,
            Adres = _adres,
            Inleiding = _inleiding,
            WerkervaringInstances = [.. _werkervaring],
            OpleidingInstances = [.. _opleidingInstances],
            CertificaatInstances = [.. _certificaatInstances],
            VaardigheidInstances = [.. _vaardigheidInstances],
            IsFavorited = _isFavorited,
            LastUpdated = _lastUpdated
        };
}
