using MediatR;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record GetTotalCvCountQuery : IRequest<int?>;
