using CvViewer.Api.Mappers;
using CvViewer.ApplicationServices.Requests;
using CvViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace CvViewer.Api.Features.Cvs;

[ApiController]
[Route("api/[controller]")]
public class CvsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("/api/[controller]/count/favorited")]
    public async Task<IActionResult> GetFavoritedCvCountAsync(CancellationToken cancellationToken)
        => await GetResponse<GetFavoritedCvCountRequest, int?>(new(), cancellationToken) is int count
            ? Ok(count)
            : NotFound();

    [HttpGet("/api/[controller]/count/updated-since/{since}")]
    public async Task<IActionResult> GetCvsUpdatedSince(Instant since, CancellationToken cancellationToken)
        => await GetResponse<GetCvsUpdatedSinceRequest, List<Cv>?>(new(since), cancellationToken) is List<Cv?> cvs
            ? Ok(cvs)
            : NotFound();

    [HttpGet("/api/[controller]/all")]
    public async Task<IActionResult> GetAllCvs(CancellationToken cancellationToken)
        => await GetResponse<GetAllCvsRequest, List<Cv>?>(new(), cancellationToken) is List<Cv> cvs
            ? Ok(cvs.Select(cv => cv.ToDto()))
            : NotFound();
}
