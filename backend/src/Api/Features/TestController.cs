using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CvViewer.Api.Features
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet()]
        public IActionResult GetDummy()
        {
            var result = _mediator.Send(new ApplicationServices.Requests.DummyRequest()).Result;

            return result is not null
                ? Ok(result)
                : NotFound();
        }
    }
}
