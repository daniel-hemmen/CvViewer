using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CvViewer.Api.Features;

public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator { get; }

    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected virtual async Task<TResponse> GetResponse<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    => await Mediator.Send(request, cancellationToken);
}
