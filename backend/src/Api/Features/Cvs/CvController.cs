using CvViewer.ApplicationServices.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CvViewer.Api.Features.Cvs
{
    [ApiController]
    [Route("api/[controller]")]
    public class CvController(IMediator Mediator) : ControllerBase
    {
        [HttpGet("/api/[controller]/count/favorite")]
        public async Task<IActionResult> GetFavoriteCount()
        {
            var request = new GetFavoritedCvCountRequest();

            var result = Mediator.Send(request);

            return result is null
                ? NotFound()
                : Ok(result);
        }
    }
}
