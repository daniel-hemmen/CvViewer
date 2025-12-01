using CvViewer.ApplicationServices.Requests;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers;

public sealed class GetTotalCvCountRequestHandler : IRequestHandler<GetTotalCvCountRequest, int?>
{
    private readonly ICvRepository _cvRepository;

    public GetTotalCvCountRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<int?> Handle(GetTotalCvCountRequest _, CancellationToken cancellationToken)
        => await _cvRepository.GetFavoritedCvCountAsync(cancellationToken);
}
