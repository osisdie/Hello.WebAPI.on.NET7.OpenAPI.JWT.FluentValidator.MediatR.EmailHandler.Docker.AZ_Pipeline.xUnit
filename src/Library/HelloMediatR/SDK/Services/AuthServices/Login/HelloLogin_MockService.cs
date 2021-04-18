using System.Threading.Tasks;
using CoreFX.Abstractions.Bases;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Extensions;
using Hello.MediatR.Domain.Contract.AuthServices.Login;
using Microsoft.Extensions.Logging;

namespace Hello.MediatR.Domain.SDK.Services.AuthServices.Login
{
    public class HelloLogin_MockService : DomainServiceBase, IAuthLogin_Service
    {
        public HelloLogin_MockService(ILogger<HelloLogin_MockService> logger)
            : base(logger)
        {

        }

        public async Task<ISvcResponseBaseDto<HelloLogin_ResponseDto>> ExecuteAsync(HelloLogin_RequestDto requestDto)
        {
            await Task.CompletedTask;
            return new SvcResponseDto<HelloLogin_ResponseDto>
            {
                Data = new HelloLogin_ResponseDto
                {
                    UserName = requestDto.UserName,
                    UserId = requestDto.UserName.ToMD5()
                }
            }.Success();
        }
    }
}
