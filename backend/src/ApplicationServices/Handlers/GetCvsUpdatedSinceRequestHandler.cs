using CvViewer.ApplicationServices.Requests;
using CvViewer.Domain;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers;

public sealed class GetCvsUpdatedSinceRequestHandler : IRequestHandler<GetCvsUpdatedSinceRequest, List<Cv>?>
{
    private readonly ICvRepository _cvRepository;

    public GetCvsUpdatedSinceRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<List<Cv>?> Handle(GetCvsUpdatedSinceRequest request, CancellationToken cancellationToken)
        => await _cvRepository.GetCvsUpdatedSinceAsync(request.Since, cancellationToken);
}
