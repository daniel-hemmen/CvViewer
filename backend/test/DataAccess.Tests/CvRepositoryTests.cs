using CvViewer.DataAccess.Mappers;
using CvViewer.Domain;
using CvViewer.Test.Shared;
using CvViewer.Test.Shared.Builder;
using NodaTime;
using Shouldly;

namespace CvViewer.DataAccess.Tests;

[TestClass]
public sealed class CvRepositoryTests
{
    [TestMethod]
    public async Task GetCvsUpdatedSinceAsync_WhenCvsExist_ReturnsExpected()
    {
        // Arrange
        var context = new TestCvContextFactory().CreateContext();
        var repo = new CvRepository(context);
        var builder = CvBuilder.CreateDefault();
        var since = SystemClock.Instance.GetCurrentInstant();

        List<Cv> cvsUpdatedSinceProvidedSince = [
            builder.WithLastUpdated(since).Build(),
            builder.WithLastUpdated(since.PlusTicks(1)).Build()];

        var cvUpdatedBeforeProvidedSince = builder.WithLastUpdated(since.PlusTicks(-1)).Build();

        context.AddRange(cvsUpdatedSinceProvidedSince.ConvertAll(cv => cv.ToEntity()));
        context.Add(cvUpdatedBeforeProvidedSince.ToEntity());
        context.SaveChanges();

        // Act
        var result = await repo.GetCvsUpdatedSinceAsync(since, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(cvsUpdatedSinceProvidedSince.Count);
        result.ForEach(actual =>
        {
            var expected = cvsUpdatedSinceProvidedSince.SingleOrDefault(expected => expected.Id == actual.Id);
            actual.ShouldBeEquivalentTo(expected);
        });
    }
}
