using ClosedXML.Excel;
using CvViewer.ApplicationServices.Extensions;
using CvViewer.ApplicationServices.FileReader.DTOs;
using CvViewer.ApplicationServices.Mappers;
using CvViewer.Domain;
using NodaTime;

namespace CvViewer.ApplicationServices.FileReader;

public sealed class CvFileReader : ICvFileReader
{
    public Cv ReadCvFromFile(Stream fileStream)
    {
        var workbook = new XLWorkbook(fileStream);

        var meta = ReadMeta(workbook.MetaSheet);
        var adres = ReadAdres(workbook.AdresSheet);
        var auteur = ReadAuteur(workbook.AuteurSheet);
        var contactgegevens = ReadContactGegevens(workbook.ContactgegevensSheet);
        var certificaten = ReadCertificaten(workbook.CertificatenSheet);
        var opleidingen = ReadOpleidingen(workbook.OpleidingenSheet);
        var vaardigheden = ReadVaardigheden(workbook.VaardighedenSheet);
        var werkervaring = ReadWerkervaring(workbook.WerkervaringSheet);

        var cv = MapToCv(meta, adres, auteur, contactgegevens, certificaten, opleidingen, vaardigheden, werkervaring);

        return cv;
    }

    private static MetaDto ReadMeta(IXLWorksheet worksheet)
        => new(Inleiding: worksheet.GetTextFromCellBelow(nameof(MetaDto.Inleiding)) ?? "");

    private static AdresDto ReadAdres(IXLWorksheet worksheet)
        => new(
            Straat: worksheet.GetTextFromCellBelow(nameof(AdresDto.Straat)) ?? "",
            Huisnummer: worksheet.GetTextFromCellBelow(nameof(AdresDto.Huisnummer)) ?? "",
            Plaats: worksheet.GetTextFromCellBelow(nameof(AdresDto.Plaats)) ?? "",
            Postcode: worksheet.GetTextFromCellBelow(nameof(AdresDto.Postcode)) ?? "",
            Land: worksheet.GetTextFromCellBelow(nameof(AdresDto.Land)) ?? "");

    private static AuteurDto ReadAuteur(IXLWorksheet worksheet)
        => new(
            Voornaam: worksheet.GetTextFromCellBelow(nameof(AuteurDto.Voornaam)) ?? "",
            Tussenvoegsel: worksheet.GetTextFromCellBelow(nameof(AuteurDto.Tussenvoegsel)) ?? "",
            Achternaam: worksheet.GetTextFromCellBelow(nameof(AuteurDto.Achternaam)) ?? "");

    private static ContactgegevensDto ReadContactGegevens(IXLWorksheet worksheet)
        => new(
            Email: worksheet.GetTextFromCellBelow(nameof(ContactgegevensDto.Email)) ?? "",
            Telefoonnummer: worksheet.GetTextFromCellBelow(nameof(ContactgegevensDto.Telefoonnummer)) ?? "",
            LinkedinUrl: worksheet.GetTextFromCellBelow(nameof(ContactgegevensDto.LinkedinUrl)) ?? "");

    private static List<CertificaatDto> ReadCertificaten(IXLWorksheet worksheet)
    {
        List<CertificaatDto> certificaten = [];

        var rows = worksheet.RowsUsed().Skip(1); // Skip header row

        foreach (var row in rows)
        {
            var rowNumber = row.RowNumber();

            var certificaat = new CertificaatDto(
                Naam: worksheet.GetTextByHeaderAndRowNumber(nameof(CertificaatDto.Naam), rowNumber) ?? "",
                Uitgever: worksheet.GetTextByHeaderAndRowNumber(nameof(CertificaatDto.Uitgever), rowNumber) ?? "",
                DatumAfgifteDag: worksheet.GetNumberByHeaderAndRowNumber(nameof(CertificaatDto.DatumAfgifteDag), rowNumber),
                DatumAfgifteMaand: worksheet.GetNumberByHeaderAndRowNumber(nameof(CertificaatDto.DatumAfgifteMaand), rowNumber),
                DatumAfgifteJaar: worksheet.GetNumberByHeaderAndRowNumber(nameof(CertificaatDto.DatumAfgifteJaar), rowNumber)!.Value,
                VerloopdatumDag: worksheet.GetNumberByHeaderAndRowNumber(nameof(CertificaatDto.VerloopdatumDag), rowNumber),
                VerloopdatumMaand: worksheet.GetNumberByHeaderAndRowNumber(nameof(CertificaatDto.VerloopdatumMaand), rowNumber),
                VerloopdatumJaar: worksheet.GetNumberByHeaderAndRowNumber(nameof(CertificaatDto.VerloopdatumJaar), rowNumber),
                Url: worksheet.GetTextByHeaderAndRowNumber(nameof(CertificaatDto.Url), rowNumber) ?? "");

            certificaten.Add(certificaat);
        }

        return certificaten;
    }

    private static List<OpleidingDto> ReadOpleidingen(IXLWorksheet worksheet)
    {
        List<OpleidingDto> opleidingen = [];

        var rows = worksheet.RowsUsed().Skip(1); // Skip header row

        foreach (var row in rows)
        {
            var rowNumber = row.RowNumber();

            var opleiding = new OpleidingDto(
                Naam: worksheet.GetTextByHeaderAndRowNumber(nameof(OpleidingDto.Naam), rowNumber) ?? "",
                Instituut: worksheet.GetTextByHeaderAndRowNumber(nameof(OpleidingDto.Instituut), rowNumber) ?? "",
                StartdatumDag: worksheet.GetNumberByHeaderAndRowNumber(nameof(OpleidingDto.StartdatumDag), rowNumber),
                StartdatumMaand: worksheet.GetNumberByHeaderAndRowNumber(nameof(OpleidingDto.StartdatumMaand), rowNumber),
                StartdatumJaar: worksheet.GetNumberByHeaderAndRowNumber(nameof(OpleidingDto.StartdatumJaar), rowNumber),
                EinddatumDag: worksheet.GetNumberByHeaderAndRowNumber(nameof(OpleidingDto.EinddatumDag), rowNumber),
                EinddatumMaand: worksheet.GetNumberByHeaderAndRowNumber(nameof(OpleidingDto.EinddatumMaand), rowNumber),
                EinddatumJaar: worksheet.GetNumberByHeaderAndRowNumber(nameof(OpleidingDto.EinddatumJaar), rowNumber)!.Value,
                Beschrijving: worksheet.GetTextByHeaderAndRowNumber(nameof(OpleidingDto.Beschrijving), rowNumber) ?? "");

            opleidingen.Add(opleiding);
        }

        return opleidingen;
    }

    private static List<VaardigheidDto> ReadVaardigheden(IXLWorksheet worksheet)
    {
        List<VaardigheidDto> vaardigheden = [];

        var rows = worksheet.RowsUsed().Skip(1); // skip header row

        foreach (var row in rows)
        {
            var rowNumber = row.RowNumber();

            var vaardigheid = new VaardigheidDto(
                Naam: worksheet.GetTextByHeaderAndRowNumber(nameof(VaardigheidDto.Naam), rowNumber) ?? "",
                Niveau: worksheet.GetNumberByHeaderAndRowNumber(nameof(VaardigheidDto.Niveau), rowNumber)!.Value);

            vaardigheden.Add(vaardigheid);
        }

        return vaardigheden;
    }

    private static List<WerkervaringDto> ReadWerkervaring(IXLWorksheet worksheet)
    {
        List<WerkervaringDto> werkervaringen = [];

        var rows = worksheet.RowsUsed().Skip(1); // skip header row

        foreach (var row in rows)
        {
            var rowNumber = row.RowNumber();

            var werkervaring = new WerkervaringDto(
                Rol: worksheet.GetTextByHeaderAndRowNumber(nameof(WerkervaringDto.Rol), rowNumber) ?? "",
                Organisatie: worksheet.GetTextByHeaderAndRowNumber(nameof(WerkervaringDto.Organisatie), rowNumber) ?? "",
                StartdatumDag: worksheet.GetNumberByHeaderAndRowNumber(nameof(WerkervaringDto.StartdatumDag), rowNumber),
                StartdatumMaand: worksheet.GetNumberByHeaderAndRowNumber(nameof(WerkervaringDto.StartdatumMaand), rowNumber),
                StartdatumJaar: worksheet.GetNumberByHeaderAndRowNumber(nameof(WerkervaringDto.StartdatumJaar), rowNumber)!.Value,
                EinddatumDag: worksheet.GetNumberByHeaderAndRowNumber(nameof(WerkervaringDto.EinddatumDag), rowNumber),
                EinddatumMaand: worksheet.GetNumberByHeaderAndRowNumber(nameof(WerkervaringDto.EinddatumMaand), rowNumber),
                EinddatumJaar: worksheet.GetNumberByHeaderAndRowNumber(nameof(WerkervaringDto.EinddatumJaar), rowNumber),
                Beschrijving: worksheet.GetTextByHeaderAndRowNumber(nameof(WerkervaringDto.Beschrijving), rowNumber) ?? "",
                Plaats: worksheet.GetTextByHeaderAndRowNumber(nameof(WerkervaringDto.Plaats), rowNumber) ?? "");

            werkervaringen.Add(werkervaring);
        }

        return werkervaringen;
    }

    private static Cv MapToCv(
        MetaDto metaDto,
        AdresDto adresDto,
        AuteurDto auteurDto,
        ContactgegevensDto contactgegevensDto,
        List<CertificaatDto> certificaatDtos,
        List<OpleidingDto> opleidingDtos,
        List<VaardigheidDto> vaardigheidDtos,
        List<WerkervaringDto> werkervaringDtos)
    {
        var adres = adresDto.ToDomain();
        var contactgegevens = contactgegevensDto.ToDomain();
        var auteur = auteurDto.ToDomain(adres, contactgegevens);

        var cv = new Cv()
        {
            Id = Guid.NewGuid(),
            Auteur = auteur,
            Contactgegevens = contactgegevens,
            Adres = adres,
            Inleiding = metaDto.Inleiding,
            WerkervaringInstances = werkervaringDtos.ConvertAll(ToDomainMappers.ToDomain),
            OpleidingInstances = opleidingDtos.ConvertAll(ToDomainMappers.ToDomain),
            CertificaatInstances = certificaatDtos.ConvertAll(ToDomainMappers.ToDomain),
            VaardigheidInstances = vaardigheidDtos.ConvertAll(ToDomainMappers.ToDomain),
            IsFavorited = false,
            LastUpdated = SystemClock.Instance.GetCurrentInstant()
        };

        return cv;
    }
}
