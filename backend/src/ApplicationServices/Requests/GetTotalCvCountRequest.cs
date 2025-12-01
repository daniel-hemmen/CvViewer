using MediatR;

namespace CvViewer.ApplicationServices.Requests;

public sealed record GetTotalCvCountRequest : IRequest<int?>;
