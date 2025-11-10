using MediatR;
using Microsoft.AspNetCore.Mvc;
using Y25.Mediatr.Handlers;

namespace Y25.Mediatr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new PingRequest());
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await _mediator.Send(new NotifyRequest { Message = "Hello from PingController" });
            return Ok(result);
        }
    }
}
