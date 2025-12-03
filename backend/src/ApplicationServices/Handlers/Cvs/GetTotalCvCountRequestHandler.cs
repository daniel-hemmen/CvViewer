using CvViewer.ApplicationServices.Requests.Cvs;
using MediatR;

namespace CvViewer.ApplicationServices.Handlers.Cvs;

public sealed class GetTotalCvCountRequestHandler : IRequestHandler<GetTotalCvCountQuery, int?>
{
    private readonly ICvRepository _cvRepository;

    public GetTotalCvCountRequestHandler(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
    }

    public async Task<int?> Handle(GetTotalCvCountQuery _, CancellationToken cancellationToken)
        => await _cvRepository.GetTotalCvCountAsync(cancellationToken);
}
