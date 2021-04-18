using System.Threading.Tasks;
using CoreFX.Abstractions.Bases.Interfaces;
using Hello.MediatR.Domain.Contract.AuthServices.Login;

namespace Hello.MediatR.Domain.SDK.Services.AuthServices.Login
{
    public interface IAuthLogin_Service : IDomainServiceBase
    {
        public Task<ISvcResponseBaseDto<HelloLogin_ResponseDto>> ExecuteAsync(HelloLogin_RequestDto requestDto);
    }
}
