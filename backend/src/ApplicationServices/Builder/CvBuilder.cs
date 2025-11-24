using CvViewer.Domain;

namespace CvViewer.ApplicationServices.Builder;

public sealed class CvBuilder
{
    private Auteur _auteur = new Auteur
    {
        Voornaam = "John",
        Achternaam = "Doe",
        Adres = null,
        Contactgegevens = null
    };

    private Contactgegevens? _contactgegevens;
    private Adres? _adres;
    private string? _inleiding;
    private readonly List<WerkervaringInstance> _werkervaring = new();
    private readonly List<OpleidingInstance> _opleidingen = new();
    private readonly List<CertificaatInstance> _certificaten = new();
    private readonly List<VaardigheidInstance> _vaardigheden = new();
    private CvMetadata? _metadata;

    public static CvBuilder Create() => new CvBuilder();

    public static Cv CreateSample()
    {
        return Create()
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
            .AddWerkervaring(new WerkervaringInstance
            {
                Bedrijfsnaam = "Contoso",
                Functietitel = "Senior Developer",
                StartDatum = DateTime.UtcNow.AddYears(-3),
                EindDatum = null,
                Locatie = "Amsterdam",
                Beschrijving = "Leiding over backend team en architectuur"
            })
            .AddOpleiding(new OpleidingInstance
            {
                Naam = "Computer Science",
                Instituut = "Universiteit van Amsterdam",
                Startdatum = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-10)),
                Einddatum = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-6)),
                Beschrijving = "Bachelor of Science"
            })
            .AddCertificaat(new CertificaatInstance
            {
                Naam = "Azure Developer Associate",
                Uitgever = "Microsoft",
                DatumAfgifte = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1)),
                Verloopdatum = null,
                Url = "https://learn.microsoft.com"
            })
            .AddVaardigheid(new VaardigheidInstance { Naam = "C#", Niveau = 5 })
            .AddVaardigheid(new VaardigheidInstance { Naam = "EF Core", Niveau = 4 })
            .WithMetadata(new CvMetadata { Id = Guid.NewGuid(), AuteurNaam = "Anna Jansen", IsFavorite = false, LastUpdated = DateTime.UtcNow })
            .Build();
    }

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

    public CvBuilder AddWerkervaring(WerkervaringInstance werk)
    {
        _werkervaring.Add(werk);
        return this;
    }

    public CvBuilder AddOpleiding(OpleidingInstance opleiding)
    {
        _opleidingen.Add(opleiding);
        return this;
    }

    public CvBuilder AddCertificaat(CertificaatInstance cert)
    {
        _certificaten.Add(cert);
        return this;
    }

    public CvBuilder AddVaardigheid(VaardigheidInstance vaardigheid)
    {
        _vaardigheden.Add(vaardigheid);
        return this;
    }

    public CvBuilder WithMetadata(CvMetadata metadata)
    {
        _metadata = metadata;
        return this;
    }

    public Cv Build()
    {
        return new Cv
        {
            Auteur = _auteur,
            Contactgegevens = _contactgegevens,
            Adres = _adres,
            Inleiding = _inleiding,
            WerkervaringInstances = [.. _werkervaring],
            OpleidingInstances = [.. _opleidingen],
            CertificaatInstances = [.. _certificaten],
            VaardigheidInstances = [.. _vaardigheden],
            Metadata = _metadata
        };
    }
}
