using MediatR;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record GetFavoritedCvCountQuery : IRequest<int?>;
