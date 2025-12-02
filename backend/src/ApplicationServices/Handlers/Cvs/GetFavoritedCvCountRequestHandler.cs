using CvViewer.ApplicationServices.Requests.Cvs;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Cvs;

public sealed class GetFavoritedCvCountRequestHandler : IRequestHandler<GetFavoritedCvCountRequest, int?>
{
    private readonly ICvRepository _cvRepository;

    public GetFavoritedCvCountRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<int?> Handle(GetFavoritedCvCountRequest _, CancellationToken cancellationToken)
        => await _cvRepository.GetFavoritedCvCountAsync(cancellationToken);
}
