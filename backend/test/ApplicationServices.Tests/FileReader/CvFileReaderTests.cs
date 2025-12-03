using ClosedXML.Excel;
using CvViewer.ApplicationServices.FileReader;
using NodaTime;
using Shouldly;

namespace CvViewer.ApplicationServices.Tests.FileReader;

[TestClass]
public sealed class CvFileReaderTests
{
    [TestMethod]
    public void ReadCvFromFile_WhenWorkbookIsValid_ReturnsMappedCv()
    {
        // Arrange
        using var workbook = CreateTestWorkbook();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        var reader = new CvFileReader();

        // Act
        var cv = reader.ReadCvFromFile(stream);

        // Assert
        cv.ShouldNotBeNull();
        cv.Auteur.ShouldNotBeNull();
        cv.Auteur.Voornaam.ShouldBe("John");
        cv.Auteur.Achternaam.ShouldBe("Doe");

        cv.Contactgegevens.ShouldNotBeNull();
        cv.Contactgegevens!.Email.ShouldBe("john.doe@example.com");
        cv.Contactgegevens.Telefoonnummer.ShouldBe("+1-555-0100");

        cv.Adres.ShouldNotBeNull();
        cv.Adres!.Plaats.ShouldBe("Amsterdam");
        cv.Adres.Land.ShouldBe("Netherlands");

        cv.Inleiding.ShouldNotBeNull();
        cv.Inleiding.ShouldContain("software");

        cv.WerkervaringInstances.ShouldNotBeNull();
        cv.WerkervaringInstances.Count.ShouldBe(1);
        cv.WerkervaringInstances[0].Organisatie.ShouldBe("Acme Corp");
        cv.WerkervaringInstances[0].Rol.ShouldBe("Developer");

        cv.VaardigheidInstances.ShouldNotBeNull();
        cv.VaardigheidInstances.Count.ShouldBe(1);
        cv.VaardigheidInstances[0].Naam.ShouldBe("C#");
        cv.VaardigheidInstances[0].Niveau.ShouldBe((byte)4);

        (SystemClock.Instance.GetCurrentInstant() - cv.LastUpdated).TotalSeconds.ShouldBeLessThan(10);
    }

    private static XLWorkbook CreateTestWorkbook()
    {
        var workbook = new XLWorkbook();

        var meta = workbook.AddWorksheet("Meta");
        meta.Cell(1, 1).Value = "Inleiding";
        meta.Cell(2, 1).Value = "Experienced software engineer with a focus on automated testing and clean code.";

        var adres = workbook.AddWorksheet("Adres");
        adres.Cell(1, 1).Value = "Straat";
        adres.Cell(1, 2).Value = "Huisnummer";
        adres.Cell(1, 3).Value = "Plaats";
        adres.Cell(1, 4).Value = "Postcode";
        adres.Cell(1, 5).Value = "Land";
        adres.Cell(2, 1).Value = "Main St";
        adres.Cell(2, 2).Value = "1";
        adres.Cell(2, 3).Value = "Amsterdam";
        adres.Cell(2, 4).Value = "1000AA";
        adres.Cell(2, 5).Value = "Netherlands";

        var auteur = workbook.AddWorksheet("Auteur");
        auteur.Cell(1, 1).Value = "Voornaam";
        auteur.Cell(1, 2).Value = "Tussenvoegsel";
        auteur.Cell(1, 3).Value = "Achternaam";
        auteur.Cell(2, 1).Value = "John";
        auteur.Cell(2, 2).Value = "";
        auteur.Cell(2, 3).Value = "Doe";

        var contact = workbook.AddWorksheet("Contactgegevens");
        contact.Cell(1, 1).Value = "Email";
        contact.Cell(1, 2).Value = "Telefoonnummer";
        contact.Cell(1, 3).Value = "LinkedinUrl";
        contact.Cell(2, 1).Value = "john.doe@example.com";
        contact.Cell(2, 2).Value = "+1-555-0100";
        contact.Cell(2, 3).Value = "";

        var vaardigheden = workbook.AddWorksheet("Vaardigheden");
        vaardigheden.Cell(1, 1).Value = "Naam";
        vaardigheden.Cell(1, 2).Value = "Niveau";
        vaardigheden.Cell(2, 1).Value = "C#";
        vaardigheden.Cell(2, 2).Value = "4";

        var werk = workbook.AddWorksheet("Werkervaring");
        werk.Cell(1, 1).Value = "Rol";
        werk.Cell(1, 2).Value = "Organisatie";
        werk.Cell(1, 3).Value = "StartdatumDag";
        werk.Cell(1, 4).Value = "StartdatumMaand";
        werk.Cell(1, 5).Value = "StartdatumJaar";
        werk.Cell(1, 6).Value = "EinddatumDag";
        werk.Cell(1, 7).Value = "EinddatumMaand";
        werk.Cell(1, 8).Value = "EinddatumJaar";
        werk.Cell(1, 9).Value = "Beschrijving";
        werk.Cell(1, 10).Value = "Plaats";
        werk.Cell(2, 1).Value = "Developer";
        werk.Cell(2, 2).Value = "Acme Corp";
        werk.Cell(2, 3).Value = "1";
        werk.Cell(2, 4).Value = "1";
        werk.Cell(2, 5).Value = "2020";
        werk.Cell(2, 6).Value = "";
        werk.Cell(2, 7).Value = "";
        werk.Cell(2, 8).Value = "";
        werk.Cell(2, 9).Value = "Worked on various backend services.";
        werk.Cell(2, 10).Value = "Amsterdam";

        var opleidingen = workbook.AddWorksheet("Opleidingen");
        opleidingen.Cell(1, 1).Value = "Naam";
        opleidingen.Cell(1, 2).Value = "Instituut";
        opleidingen.Cell(1, 3).Value = "StartdatumDag";
        opleidingen.Cell(1, 4).Value = "StartdatumMaand";
        opleidingen.Cell(1, 5).Value = "StartdatumJaar";
        opleidingen.Cell(1, 6).Value = "EinddatumDag";
        opleidingen.Cell(1, 7).Value = "EinddatumMaand";
        opleidingen.Cell(1, 8).Value = "EinddatumJaar";
        opleidingen.Cell(1, 9).Value = "Beschrijving";
        opleidingen.Cell(2, 1).Value = "Computer Science";
        opleidingen.Cell(2, 2).Value = "University";
        opleidingen.Cell(2, 3).Value = "";
        opleidingen.Cell(2, 4).Value = "";
        opleidingen.Cell(2, 5).Value = "2014";
        opleidingen.Cell(2, 6).Value = "";
        opleidingen.Cell(2, 7).Value = "";
        opleidingen.Cell(2, 8).Value = "2018";
        opleidingen.Cell(2, 9).Value = "Bachelor degree";

        var certs = workbook.AddWorksheet("Certificaten");
        certs.Cell(1, 1).Value = "Naam";
        certs.Cell(1, 2).Value = "Uitgever";
        certs.Cell(1, 3).Value = "DatumAfgifteDag";
        certs.Cell(1, 4).Value = "DatumAfgifteMaand";
        certs.Cell(1, 5).Value = "DatumAfgifteJaar";
        certs.Cell(1, 6).Value = "VerloopdatumDag";
        certs.Cell(1, 7).Value = "VerloopdatumMaand";
        certs.Cell(1, 8).Value = "VerloopdatumJaar";
        certs.Cell(1, 9).Value = "Url";
        certs.Cell(2, 1).Value = "Cert A";
        certs.Cell(2, 2).Value = "Issuer";
        certs.Cell(2, 3).Value = "1";
        certs.Cell(2, 4).Value = "1";
        certs.Cell(2, 5).Value = "2021";
        certs.Cell(2, 6).Value = "";
        certs.Cell(2, 7).Value = "";
        certs.Cell(2, 8).Value = "";
        certs.Cell(2, 9).Value = "";

        return workbook;
    }
}
