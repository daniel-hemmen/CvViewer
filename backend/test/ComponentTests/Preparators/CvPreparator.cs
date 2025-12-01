using CvViewer.ApplicationServices;
using CvViewer.Domain;
using CvViewer.Test.Shared.Builder;

namespace CvViewer.ComponentTests.Preparators;

public sealed class CvPreparator
{
    private readonly ICvRepository _cvRepository;

    public CvPreparator(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<Guid> PrepareCvAsync(CancellationToken cancellationToken)
    {
        var cv = CvBuilder.CreateDefault().Build();

        await _cvRepository.AddCvAsync(cv, cancellationToken);

        return cv.Id;
    }


    public async Task PrepareCvsAsync(int count, CancellationToken cancellationToken)
    {
        var cvs = CreateCvs(count, withIsFavorited: false);

        await _cvRepository.AddCvsAsync(cancellationToken, [.. cvs]);
    }

    public async Task PrepareFavoritedCvsAsync(int count, CancellationToken cancellationToken)
    {
        var cvs = CreateCvs(count, withIsFavorited: true);

        await _cvRepository.AddCvsAsync(cancellationToken, [.. cvs]);
    }

    private static List<Cv> CreateCvs(int count, bool withIsFavorited = false)
        => [.. Enumerable.Range(0, count)
                .Select(_ => CvBuilder.CreateDefault().WithIsFavorited(withIsFavorited)
                .Build())];
}
