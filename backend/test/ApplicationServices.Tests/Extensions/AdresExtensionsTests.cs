using CvViewer.Domain.Extensions;
using Shouldly;

namespace CvViewer.ApplicationServices.Tests.Extensions;

[TestClass]
public sealed class AdresExtensionsTests
{
    [TestMethod]
    [DataRow("Voorstraat", "123A", "1234AB", "Plaatsnaam")]
    [DataRow("Another St.", "45", "5678CD", "Othercity")]
    public void FullAdres_WhenCalled_ThenReturnsFormattedAddress(string straat, string huisnummer, string postcode, string plaats)
    {
        // Arrange
        var adres = new Domain.Adres
        {
            Straat = straat,
            Huisnummer = huisnummer,
            Postcode = postcode,
            Plaats = plaats,
            Land = "NL"
        };

        var expected = $"{straat} {huisnummer}, {postcode} {plaats}";

        // Act
        var result = adres.FullAdres;

        // Assert
        result.ShouldBe(expected);
    }
}
