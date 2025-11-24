using CvViewer.ApplicationServices.DTOs;
using MediatR;

namespace CvViewer.ApplicationServices.Requests
{
    public sealed record DummyRequest() : IRequest<CvDto>;
}
