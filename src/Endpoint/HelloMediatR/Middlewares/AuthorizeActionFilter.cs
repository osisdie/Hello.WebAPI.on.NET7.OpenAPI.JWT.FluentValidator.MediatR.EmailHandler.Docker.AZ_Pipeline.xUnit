using System;
using System.Net;
using CoreFX.Auth.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hello.MediatR.Endpoint.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeActionFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items[JwtConst.JwtTokenItemName];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = HttpStatusCode.Unauthorized.ToString() }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
