using System.Net;
using System.Threading.Tasks;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Enums;
using CoreFX.Abstractions.Extensions;
using CoreFX.Auth.Models;
using CoreFX.Notification.Models;
using Hello.MediatR.Domain.Contract;
using Hello.MediatR.Domain.Contract.AuthServices.Login;
using Hello.MediatR.Endpoint.Controllers.Bases;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hello.MediatR.Endpoint.Controllers.AuthActions
{
    public partial class AuthController : DomainContollerBase
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="requestDto">Username and password</param>
        /// <returns></returns>
        [ApiVersionNeutral]
        [ApiExplorerSettings(GroupName = "latest")]
        [Route("api/auth/login")] // latest
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ISvcResponseBaseDto<JwtTokenDto>))]
        public async Task<IActionResult> Login_latest([FromBody] HelloLogin_RequestDto requestDto) => await Login_v202104(requestDto);

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="requestDto">Username and password</param>
        /// <returns></returns>
        [ApiVersion("202104")]
        [ApiExplorerSettings(GroupName = "v202104")]
        [Route("api/v202104/auth/login")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ISvcResponseBaseDto<JwtTokenDto>))]
        public async Task<IActionResult> Login_v202104([FromBody] HelloLogin_RequestDto requestDto) => await Login_v202103(requestDto);

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="requestDto">Username and password</param>
        /// <returns></returns>
        [ApiVersion("202103")]
        [ApiExplorerSettings(GroupName = "v202103")]
        [Route("api/v202103/auth/login")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ISvcResponseBaseDto<JwtTokenDto>))]
        public async Task<IActionResult> Login_v202103([FromBody] HelloLogin_RequestDto requestDto)
        {
            var svcRes = await _svcLogin.ExecuteAsync(requestDto);
            if (!svcRes.Any())
            {
                svcRes.SetSubCode(HelloMediatRSvcCodeEnum.UnAuthenticated);

                _mediator?.Publish(new SvcEvent_MetadataDto
                {
                    From = HttpContext.Request.GetDisplayUrl(),
                    Category = "User login",
                    IsSuccess = svcRes.Any(),
                    User = requestDto.UserName,
                    RequestDto = requestDto,
                    ResponseDto = svcRes,
                    Context = HttpContext
                });

                return new UnauthorizedObjectResult(svcRes);
            }

            var jwtRes = await _sessionAdmin.Authentication(requestDto.UserName, requestDto.Password);
            if (!jwtRes.Any())
            {
                svcRes.SetSubCode(SvcCodeEnum.TokenCreateFailed);
                return new UnauthorizedObjectResult(jwtRes);
            }

            var res = new SvcResponseDto<HelloLogin_ResponseDto>
            {
                Data = new HelloLogin_ResponseDto
                {
                    UserId = jwtRes.Data.UserId,
                    UserName = jwtRes.Data.UserName,
                    Token = jwtRes.Data.Token,
                    RefreshToken = jwtRes.Data.RefreshToken,
                    Exp = jwtRes.Data.Exp,
                }
            }.Success();

            return Ok(res);
        }
    }
}
