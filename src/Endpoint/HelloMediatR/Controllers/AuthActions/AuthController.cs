using CoreFX.Auth.Interfaces;
using Hello.MediatR.Domain.SDK.Services.AuthServices.Login;
using Hello.MediatR.Endpoint.Controllers.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hello.MediatR.Endpoint.Controllers.AuthActions
{
    [ApiController]
    public partial class AuthController : DomainContollerBase
    {
        protected readonly IAuthLogin_Service _svcLogin;
        protected readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger,
            ISessionAdmin sessionAdmin,
            IMediator mediator = null,
            IAuthLogin_Service svcLogin = null) : base(logger, sessionAdmin)
        {
            _mediator = mediator;
            _svcLogin = svcLogin;
        }
    }
}
