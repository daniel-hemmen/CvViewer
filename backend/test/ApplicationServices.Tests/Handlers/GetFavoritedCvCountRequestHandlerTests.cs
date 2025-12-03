using CvViewer.ApplicationServices.Handlers.Cvs;
using CvViewer.ApplicationServices.Requests.Cvs;
using Moq;
using Shouldly;

namespace CvViewer.ApplicationServices.Tests.Handlers;

[TestClass]
public sealed class GetFavoritedCvCountRequestHandlerTests
{
    private readonly ICvRepository _cvRepository = Mock.Of<ICvRepository>();

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(100)]
    public async Task Handle_WhenCalled_ThenCallsRepository(int expected)
    {
        // Arrange
        var handler = new GetFavoritedCvCountRequestHandler(_cvRepository);

        var request = new GetFavoritedCvCountQuery();

        Mock.Get(_cvRepository)
            .Setup(repo => repo.GetFavoritedCvCountAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await handler.Handle(request, CancellationToken.None);

        // Assert
        actual.ShouldBe(expected);
    }
}
