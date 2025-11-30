using CvViewer.Shared.Extensions;
using Shouldly;

namespace CvViewer.Domain.Tests.Extensions;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    [DataRow("test", "Test")]
    [DataRow("Test", "Test")]
    [DataRow("1aBc", "1aBc")]
    public void Capitalize_WhenCalled_ShouldReturnExpected(string input, string expected)
    {
        // Act
        var result = input.Capitalize();

        // Assert
        result.ShouldBe(expected);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("\t")]
    public void Capitalize_WhenInputNullOrEmpty_ThenShouldThrow(string input)
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() => input!.Capitalize());
    }
}
