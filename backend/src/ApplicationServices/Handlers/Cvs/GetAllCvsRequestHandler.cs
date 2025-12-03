using CvViewer.ApplicationServices.Requests.Cvs;
using CvViewer.Domain;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Cvs;

public sealed class GetAllCvsRequestHandler : IRequestHandler<GetAllCvsQuery, List<Cv>?>
{
    private readonly ICvRepository _cvRepository;

    public GetAllCvsRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<List<Cv>?> Handle(GetAllCvsQuery _, CancellationToken cancellationToken)
        => await _cvRepository.GetAllCvsAsync(cancellationToken);
}
