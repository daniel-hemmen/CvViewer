using CvViewer.ApplicationServices;
using Shouldly;

namespace CvViewer.ComponentTests.Validators;

public sealed class CvValidator
{
    private readonly ICvRepository _cvRepository;

    public CvValidator(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task ShouldBeFavoritedAsync(Guid cvExternalId, CancellationToken cancellationToken)
    {
        var result = await _cvRepository.GetCvByIdAsync(cvExternalId, cancellationToken);

        result.ShouldNotBeNull();
        result.IsFavorited.ShouldBeTrue();
    }
}
