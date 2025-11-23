using Shouldly;

namespace CvViewer.Types.Tests;

[TestClass]
public sealed class SynchronizedDateTimeTests
{
    [TestMethod]
    [DataRow(DateTimeKind.Local)]
    [DataRow(DateTimeKind.Utc)]
    public void FromDateTime_WhenKindSpecified_ThenShouldHaveExpectedProperties(DateTimeKind dateTimeKind)
    {
        // Arrange
        var dateTime = DateTime.SpecifyKind(DateTime.Now, dateTimeKind);

        // Act
        var sut = SynchronizedDateTime.FromDateTime(dateTime);

        // Assert
        sut.Local.ShouldBe(dateTime.ToLocalTime());
        sut.Utc.ShouldBe(dateTime);
    }

    [TestMethod]
    public void FromDateTime_WhenKindUnSpecified_ThenShouldThrow()
    {
        // Arrange
        var dateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        var expectedMessage = "DateTimeKind must be specified";

        // Act
        var exception = Should.Throw<NotSupportedException>(() => SynchronizedDateTime.FromDateTime(dateTime));

        // Assert
        exception.Message.ShouldBe(expectedMessage);
    }


    [TestMethod]
    [DataRow(DateTimeKind.Local)]
    [DataRow(DateTimeKind.Unspecified)]
    public void Local_WhenSetWithUnspecifiedOrLocalKind_ThenShouldUpdatePropertiesAccordingly(DateTimeKind dateTimeKind)
    {
        // Arrange
        var initialDateTime = DateTime.Now;
        var sut = SynchronizedDateTime.FromDateTime(initialDateTime);

        var newDateTime = DateTime.SpecifyKind(initialDateTime.AddTicks(1), dateTimeKind);

        // Act
        sut.Local = newDateTime;

        // Assert
        sut.Local.ShouldBe(newDateTime);
        sut.Utc.ShouldBe(newDateTime.ToUniversalTime());
    }

    [TestMethod]
    public void Local_WhenSetWithUtcDateTime_ThenShouldThrow()
    {
        // Arrange
        var initialLocalDateTime = DateTime.Now;
        var sut = SynchronizedDateTime.FromDateTime(initialLocalDateTime);

        var expectedMessage = "Local time cannot be set with UTC DateTime";

        var utcDateTime = DateTime.UtcNow;

        // Act
        var exception = Should.Throw<ArgumentException>(() => sut.Local = utcDateTime);

        // Assert
        exception.Message.ShouldBe(expectedMessage);
    }

    [TestMethod]
    public void Utc_WhenSet_ThenShouldSetSource()
    {
        // Arrange
        var sut = SynchronizedDateTime.FromDateTime(DateTime.UtcNow);

        // Act

        // Assert
    }
}
