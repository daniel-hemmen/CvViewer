using CvViewer.ApplicationServices.Requests;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers;

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
