using CvViewer.ApplicationServices.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CvViewer.Api.Features.Authors
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IMediator Mediator) : ControllerBase
    {
        [HttpGet("/auteur/{cvId}")]
        public async Task<IActionResult> GetAuthorByCvId(long cvId)
        {
            var request = new GetAuthorByCvIdRequest(cvId.ToString());

            var result = Mediator.Send(request);

            return result is null
                ? NotFound()
                : Ok(result);
        }
    }
}
