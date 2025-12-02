using MediatR;

namespace CvViewer.ApplicationServices.Requests.Cvs;

public sealed record GetTotalCvCountRequest : IRequest<int?>;
