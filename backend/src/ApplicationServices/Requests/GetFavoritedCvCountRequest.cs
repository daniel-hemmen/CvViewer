using MediatR;

namespace CvViewer.ApplicationServices.Requests
{
    public sealed record GetFavoritedCvCountRequest : IRequest<int>;
}
