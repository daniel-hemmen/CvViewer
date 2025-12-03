using CvViewer.Api.Validators;
using CvViewer.ApplicationServices.DTOs;
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
    public async Task<IResult> GetUploadTemplateAsync(CancellationToken cancellationToken)
        => await GetResponse<GetTemplateQuery, BlobDownloadResultDto?>(new(), cancellationToken) is BlobDownloadResultDto downloadResult
            ? Results.File(
                downloadResult.Content,
                downloadResult.ContentType,
                downloadResult.FileName)
            : Results.NotFound();

    [HttpPost("/api/[controller]")]
    public async Task<IResult> AddCvFromFileAsync(CancellationToken cancellationToken)
    {
        var file = Request.Form.Files.FirstOrDefault();

        if (!CvFileValidator.TryValidate(file, out var problemDetails))
        {
            return Results.Problem(problemDetails);
        }

        await using var stream = file!.OpenReadStream();

        var success = await _mediator.Send(new AddCvFromFileCommand(stream), cancellationToken);

        return success
            ? Results.Accepted()
            : Results.UnprocessableEntity();
    }

    private async Task<TResponse> GetResponse<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    => await _mediator.Send(request, cancellationToken);
}
