using System;
using System.Threading.Tasks;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Extensions;
using Hello.MediatR.Endpoint.Controllers.Bases;
using Hello.MediatR.Endpoint.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hello.MediatR.Endpoint.Controllers
{
    [Route("api/echo")]
    [ApiController]
    public class EchoController : DomainContollerBase
    {
        protected readonly IMediator _mediator;

        public EchoController(ILogger<EchoController> logger,
            IMediator mediator = null) : base(logger, null)
        {
            _mediator = mediator;
        }

        [Route("ver")]
        [Route("version")]
        [HttpGet]
        public async Task<IActionResult> EchoVersion()
        {
            var res = new SvcResponseDto<string>
            {
                Data = SdkRuntime.Version
            }.Success();

            await Task.CompletedTask;
            return Ok(res);
        }

        [AuthorizeActionFilter]
        [Route("throw")]
        [HttpGet]
        public Task<IActionResult> EchoThrow()
        {
            throw new NotImplementedException();
        }
    }
}
