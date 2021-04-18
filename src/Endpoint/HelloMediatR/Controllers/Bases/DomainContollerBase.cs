using System.Net;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Logging;
using CoreFX.Auth.Interfaces;
using CoreFX.Hosting.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Hello.MediatR.Endpoint.Controllers.Bases
{
    [ValidationActionFilter]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ISvcResponseBaseDto))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ISvcResponseBaseDto))]
    public abstract class DomainContollerBase : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly ISessionAdmin _sessionAdmin;

        protected DomainContollerBase()
            : this(null)
        {

        }

        protected DomainContollerBase(ILogger logger)
            : this(logger, null)
        {

        }

        protected DomainContollerBase(ILogger logger, ISessionAdmin sessionAdmin)
        {
            _sessionAdmin = sessionAdmin;
            _logger = logger ?? LogMgr.CreateLogger(GetType());
        }
    }
}
