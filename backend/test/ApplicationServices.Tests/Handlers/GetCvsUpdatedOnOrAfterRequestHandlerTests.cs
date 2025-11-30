using CvViewer.ApplicationServices.Handlers;
using CvViewer.ApplicationServices.Requests;
using CvViewer.Domain;
using CvViewer.Test.Shared.Builder;
using Moq;
using NodaTime;
using Shouldly;

namespace CvViewer.ApplicationServices.Tests.Handlers;

[TestClass]
public sealed class GetCvsUpdatedSinceRequestHandlerTests
{
    private readonly ICvRepository _cvRepository = Mock.Of<ICvRepository>();

    [TestMethod]
    public async Task Handle_WhenCalled_ThenReturnsExpectedCvs()
    {
        // Arrange
        var handler = new GetCvsUpdatedSinceRequestHandler(_cvRepository);

        var since = SystemClock.Instance.GetCurrentInstant();

        var builder = CvBuilder.CreateDefault();

        var expected = new List<Cv>
        {
            builder.WithLastUpdated(since).Build(),
            builder.WithLastUpdated(since.PlusTicks(1)).Build()
        };

        Mock.Get(_cvRepository)
            .Setup(repo => repo.GetCvsUpdatedSinceAsync(since, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var request = new GetCvsUpdatedSinceRequest(since);

        // Act
        var actual = await handler.Handle(request, CancellationToken.None);

        // Assert
        actual.ShouldBe(expected);
    }
}
