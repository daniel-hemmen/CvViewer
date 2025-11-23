using CvViewer.Shared.Extensions;
using Shouldly;

namespace CvViewer.Domain.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow("test", "Test")]
        [DataRow("Test", "Test")]
        [DataRow("", "")]
        [DataRow("1aBc", "1aBc")]
        public void Capitalize_WhenCalled_ShouldReturnExpected(string input, string expected)
        {
            // Act
            var result = input.Capitalize();

            // Assert
            result.ShouldBe(expected);
        }

        [TestMethod]
        public void Capitalize_WhenInputNull_ThenShouldThrow()
        {
            // Arrange
            var input = (string?)null;

            // Act & Assert
            Should.Throw<ArgumentException>(() => input!.Capitalize());
        }
    }
}
