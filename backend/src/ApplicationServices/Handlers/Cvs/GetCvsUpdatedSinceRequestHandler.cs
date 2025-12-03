using CvViewer.ApplicationServices.Requests.Cvs;
using CvViewer.Domain;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Cvs;

public sealed class GetCvsUpdatedSinceRequestHandler : IRequestHandler<GetCvsUpdatedSinceQuery, List<Cv>?>
{
    private readonly ICvRepository _cvRepository;

    public GetCvsUpdatedSinceRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<List<Cv>?> Handle(GetCvsUpdatedSinceQuery request, CancellationToken cancellationToken)
        => await _cvRepository.GetCvsUpdatedSinceAsync(request.Since, cancellationToken);
}
