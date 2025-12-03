using CvViewer.ApplicationServices.Requests.Cvs;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Cvs;

public sealed class ToggleCvIsFavoritedRequestHandler : IRequestHandler<ToggleCvIsFavoritedCommand, bool?>
{
    private readonly ICvRepository _cvRepository;

    public ToggleCvIsFavoritedRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<bool?> Handle(ToggleCvIsFavoritedCommand request, CancellationToken cancellationToken)
        => await _cvRepository.ToggleCvIsFavoritedAsync(request.ExternalId, cancellationToken);
}
