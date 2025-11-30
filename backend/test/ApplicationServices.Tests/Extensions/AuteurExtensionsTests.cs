using CvViewer.Domain;
using CvViewer.Domain.Extensions;
using Shouldly;

namespace CvViewer.ApplicationServices.Tests.Extensions;

[TestClass]
public sealed class AuteurExtensionsTests
{
    [TestMethod]
    [DataRow("First", "Some Infix", "Last")]
    [DataRow("First", null, "Last")]
    [DataRow("First", "\t", "Last")]
    [DataRow("First", " ", "Last")]
    public void FullName_WhenCalled_ThenReturnsFormattedName(string firstName, string infix, string lastName)
    {
        // Arrange
        var auteur = new Auteur
        {
            Voornaam = firstName,
            Tussenvoegsel = infix,
            Achternaam = lastName
        };

        var expected = string.IsNullOrWhiteSpace(infix)
            ? $"{firstName} {lastName}"
            : $"{firstName} {infix} {lastName}";

        // Act
        var result = auteur.FullName;

        // Assert
        result.ShouldBe(expected);
    }

    [TestMethod]
    [DataRow("First", "\t", "Last")]
    [DataRow("First", " ", "Last")]
    [DataRow("First", "", "Last")]
    [DataRow("First", null, "Last")]
    public void FullName_WhenTussenvoegselIsNullOrWhitespace_ThenOmitsTussenvoegsel(string firstName, string infix, string lastName)
    {
        // Arrange
        var auteur = new Auteur
        {
            Voornaam = firstName,
            Tussenvoegsel = infix,
            Achternaam = lastName
        };

        var expected = $"{firstName} {lastName}";

        // Act
        var result = auteur.FullName;

        // Assert
        result.ShouldBe(expected);
    }
}
