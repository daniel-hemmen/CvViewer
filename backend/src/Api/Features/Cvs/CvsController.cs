using CvViewer.Api.Mappers;
using CvViewer.ApplicationServices.Requests.Cvs;
using CvViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace CvViewer.Api.Features.Cvs;

[ApiController]
[Route("api/[controller]")]
public class CvsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CvsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/api/[controller]/count/favorited")]
    public async Task<IActionResult> GetFavoritedCvCountAsync(CancellationToken cancellationToken)
        => await GetResponse<GetFavoritedCvCountQuery, int?>(new(), cancellationToken) is int count
            ? Ok(count)
            : NotFound();

    [HttpGet("/api/[controller]/count/updated-since/{since}")]
    public async Task<IActionResult> GetCvsUpdatedSince(Instant since, CancellationToken cancellationToken)
        => await GetResponse<GetCvsUpdatedSinceQuery, List<Cv>?>(new(since), cancellationToken) is List<Cv?> cvs
            ? Ok(cvs)
            : NotFound();

    [HttpGet("/api/[controller]/count/total")]
    public async Task<IActionResult> GetTotalCvCountAsync(CancellationToken cancellationToken)
        => await GetResponse<GetTotalCvCountQuery, int?>(new(), cancellationToken) is int count
            ? Ok(count)
            : NotFound();

    [HttpGet("/api/[controller]/all")]
    public async Task<IActionResult> GetAllCvs(CancellationToken cancellationToken)
        => await GetResponse<GetAllCvsQuery, List<Cv>?>(new(), cancellationToken) is List<Cv> cvs
            ? Ok(cvs.Select(cv => cv.ToDto()))
            : NotFound();

    [HttpPut("/api/[controller]/togglefavorited/{externalId}")]
    public async Task<IActionResult> ToggleCvIsFavorited(Guid externalId, CancellationToken cancellationToken)
        => await GetResponse<ToggleCvIsFavoritedCommand, bool?>(new(externalId), cancellationToken) is bool isFavoritedAfterToggle
            ? Ok(isFavoritedAfterToggle)
            : NotFound();

    private async Task<TResponse> GetResponse<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    => await _mediator.Send(request, cancellationToken);
}
