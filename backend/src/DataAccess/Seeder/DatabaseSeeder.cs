using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CvViewer.DataAccess.Entities;
using CvViewer.DataAccess.Snapshots;
using CvViewer.Types;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CvViewer.DataAccess.Seeder;

public static class DatabaseSeeder
{
    public static void Seed(CvContext context)
    {
        if (context.Set<CvEntity>().Any())
            return;

        var dto = ReadFirstCvDto();
        if (dto == null)
            return;

        var entity = MapToEntity(dto);

        context.Add(entity);
        context.SaveChanges();
    }

    public static async Task SeedAsync(CvContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Set<CvEntity>().AnyAsync(cancellationToken))
            return;

        var dto = ReadFirstCvDto();
        if (dto == null)
            return;

        var entity = MapToEntity(dto);

        await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static SeedCvDto? ReadFirstCvDto()
    {
        var json = TryReadJsonFile();
        if (string.IsNullOrWhiteSpace(json))
            return null;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        try
        {
            var list = JsonSerializer.Deserialize<SeedCvDto[]>(json, options);
            return list?.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }

    private static string? TryReadJsonFile()
    {
        var candidates = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "src", "DataAccess", "Seeder", "cv-details.json"),
            Path.Combine(Directory.GetCurrentDirectory(), "DataAccess", "Seeder", "cv-details.json"),
            Path.Combine(AppContext.BaseDirectory, "src", "DataAccess", "Seeder", "cv-details.json"),
            Path.Combine(AppContext.BaseDirectory, "DataAccess", "Seeder", "cv-details.json"),
            Path.Combine(AppContext.BaseDirectory, "cv-details.json"),
        };

        foreach (var path in candidates)
        {
            if (File.Exists(path))
                return File.ReadAllText(path);
        }

        // attempt to search upward from current dir for the file
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        for (int i = 0; i < 6 && dir != null; i++)
        {
            var file = Path.Combine(dir.FullName, "src", "DataAccess", "Seeder", "cv-details.json");
            if (File.Exists(file))
                return File.ReadAllText(file);

            file = Path.Combine(dir.FullName, "DataAccess", "Seeder", "cv-details.json");
            if (File.Exists(file))
                return File.ReadAllText(file);

            dir = dir.Parent;
        }

        return null;
    }

    private static CvEntity MapToEntity(SeedCvDto dto)
    {
        // simple split of name into first/last
        var names = (dto.Name ?? string.Empty).Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        var first = names.Length > 0 ? names[0] : dto.Name ?? string.Empty;
        var last = names.Length > 1 ? names[^1] : string.Empty;

        var city = ExtractCity(dto.Location) ?? string.Empty;
        var country = ExtractCountry(dto.Location) ?? string.Empty;

        var auteur = new AuteurEntity
        {
            ExternalId = System.Guid.NewGuid(),
            Voornaam = first,
            Tussenvoegsel = names.Length > 2 ? string.Join(" ", names.Skip(1).Take(names.Length - 2)) : null,
            Achternaam = last,
            Adres = new AdresEntity
            {
                Straat = string.Empty,
                Huisnummer = string.Empty,
                Plaats = city,
                Postcode = string.Empty,
                Land = country
            },
            Contactgegevens = new ContactgegevensEntity
            {
                Email = dto.Email,
                Telefoonnummer = dto.Phone,
                LinkedInUrl = null
            }
        };

        var cv = new CvEntity
        {
            ExternalId = System.Guid.NewGuid(),
            Auteur = auteur,
            ContactgegevensSnapshot = new ContactgegevensSnapshot
            {
                Email = dto.Email,
                Telefoonnummer = dto.Phone,
                LinkedInUrl = null
            },
            AdresSnapshot = new AdresSnapshot
            {
                Straat = null,
                Huisnummer = null,
                Postcode = null,
                Stad = city,
                Land = country
            },
            WerkervaringInstances = dto.Experience?.Select(e => new WerkervaringInstanceSnapshot
            {
                Organisatie = e.Company ?? string.Empty,
                Rol = e.Position ?? string.Empty,
                Startdatum = ToDateParts(e.StartDate) ?? new DateParts(1, null, null),
                Einddatum = ToDateParts(e.EndDate),
                Beschrijving = e.Description,
                Plaats = null
            }).ToList() ?? new List<WerkervaringInstanceSnapshot>(),
            OpleidingInstances = dto.Education?.Select(ed => new OpleidingInstanceSnapshot
            {
                Naam = ed.Degree ?? string.Empty,
                Instituut = ed.Institution ?? string.Empty,
                Startdatum = ToDateParts(ed.StartDate),
                Einddatum = ToDateParts(ed.EndDate) ?? new DateParts(1, null, null),
                Beschrijving = ed.Description
            }).ToList() ?? new List<OpleidingInstanceSnapshot>(),
            CertificaatInstances = dto.Certifications?.Select(c => new CertificaatInstanceSnapshot
            {
                Naam = c.Name ?? string.Empty,
                Uitgever = c.Issuer ?? string.Empty,
                DatumAfgifte = ToDateParts(c.Date) ?? new DateParts(1, null, null),
                Verloopdatum = null,
                Url = null
            }).ToList() ?? new List<CertificaatInstanceSnapshot>(),
            VaardigheidInstances = dto.Skills?.Select(s => new VaardigheidInstanceSnapshot
            {
                Naam = s.Name,
                Niveau = s.Level != null ? (byte?)s.Level.Value : null
            }).ToList() ?? new List<VaardigheidInstanceSnapshot>(),
            Inleiding = dto.Summary,
            IsFavorite = dto.IsFavorite,
            LastUpdated = ParseLastUpdatedToInstant(dto.LastUpdated)
        };

        return cv;
    }

    private static string? ExtractCity(string? location)
        => string.IsNullOrWhiteSpace(location) ? null : location.Split(',').FirstOrDefault()?.Trim();

    private static string? ExtractCountry(string? location)
        => string.IsNullOrWhiteSpace(location) ? null : (location.Contains(',') ? location.Split(',').LastOrDefault()?.Trim() : null);

    private static DateParts? ToDateParts(DateDto? dto)
    {
        if (dto == null) return null;
        return new DateParts(dto.Year, dto.Month, dto.Day);
    }

    private static Instant ParseLastUpdatedToInstant(string? lastUpdated)
    {
        if (string.IsNullOrWhiteSpace(lastUpdated))
            return SystemClock.Instance.GetCurrentInstant();

        var dt = System.DateTime.Parse(lastUpdated);
        var utc = System.DateTime.SpecifyKind(dt, System.DateTimeKind.Utc);
        return Instant.FromDateTimeUtc(utc);
    }

    // DTOs for deserialization
    private sealed class SeedCvDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? Summary { get; set; }
        public SeedExperienceDto[]? Experience { get; set; }
        public SeedEducationDto[]? Education { get; set; }
        public SeedCertificationDto[]? Certifications { get; set; }
        public SeedSkillDto[]? Skills { get; set; }
        public object[]? Languages { get; set; }
        public string? LastUpdated { get; set; }
        public bool IsFavorite { get; set; }
    }

    private sealed class SeedExperienceDto
    {
        public string? Company { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public DateDto? StartDate { get; set; }
        public DateDto? EndDate { get; set; }
        public string? Description { get; set; }
    }

    private sealed class SeedEducationDto
    {
        public string? Institution { get; set; }
        public string? Degree { get; set; }
        public DateDto? StartDate { get; set; }
        public DateDto? EndDate { get; set; }
        public string? Description { get; set; }
    }

    private sealed class SeedCertificationDto
    {
        public string? Name { get; set; }
        public string? Issuer { get; set; }
        public DateDto? Date { get; set; }
    }

    private sealed class SeedSkillDto
    {
        public string? Name { get; set; }
        public int? Level { get; set; }
    }

    private sealed class DateDto
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
    }
}
