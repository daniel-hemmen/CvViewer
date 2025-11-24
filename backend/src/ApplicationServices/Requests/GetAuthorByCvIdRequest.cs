using CvViewer.ApplicationServices.DTOs;
using MediatR;

namespace CvViewer.ApplicationServices.Requests
{
    public sealed record GetAuthorByCvIdRequest(string Id) : IRequest<AuteurDto>;
}
