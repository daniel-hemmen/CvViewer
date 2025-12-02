using CvViewer.ApplicationServices.Handlers.Upload;
using CvViewer.ApplicationServices.Requests.Upload;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CvViewer.Api.Features.Upload;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IMediator _mediator;

    public UploadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/api/[controller]/template")]
    public async Task<IResult> GetUploadTemplate(CancellationToken cancellationToken)
        => await GetResponse<GetTemplateRequest, DownloadResult?>(new(), cancellationToken) is DownloadResult downloadResult
            ? Results.File(
                downloadResult.Content,
                downloadResult.ContentType,
                downloadResult.FileName)
            : Results.NotFound();

    private async Task<TResponse> GetResponse<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    => await _mediator.Send(request, cancellationToken);
}
