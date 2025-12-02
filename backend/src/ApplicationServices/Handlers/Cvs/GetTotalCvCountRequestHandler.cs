using CvViewer.ApplicationServices.Requests.Cvs;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Cvs;

public sealed class GetTotalCvCountRequestHandler : IRequestHandler<GetTotalCvCountRequest, int?>
{
    private readonly ICvRepository _cvRepository;

    public GetTotalCvCountRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<int?> Handle(GetTotalCvCountRequest _, CancellationToken cancellationToken)
        => await _cvRepository.GetTotalCvCountAsync(cancellationToken);
}
