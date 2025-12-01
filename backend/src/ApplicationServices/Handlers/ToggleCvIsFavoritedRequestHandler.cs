using CvViewer.ApplicationServices.Requests;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers;

public sealed class ToggleCvIsFavoritedRequestHandler : IRequestHandler<ToggleCvIsFavoritedRequest, bool?>
{
    private readonly ICvRepository _cvRepository;

    public ToggleCvIsFavoritedRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<bool?> Handle(ToggleCvIsFavoritedRequest request, CancellationToken cancellationToken)
        => await _cvRepository.ToggleCvIsFavoritedAsync(request.ExternalId, cancellationToken);
}
