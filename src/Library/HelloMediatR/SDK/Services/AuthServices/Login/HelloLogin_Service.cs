using System.Threading.Tasks;
using AutoMapper;
using CoreFX.Abstractions.Bases;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Enums;
using CoreFX.Abstractions.Extensions;
using Hello.MediatR.Domain.Contract.AuthServices.Login;
using Hello.MediatR.Domain.DataAccess.DbContexts;
using Hello.MediatR.Domain.SDK.Services.AuthServices.Login.Models;
using Microsoft.Extensions.Logging;

namespace Hello.MediatR.Domain.SDK.Services.AuthServices.Login
{
    public class HelloLogin_Service : DomainServiceBase, IAuthLogin_Service
    {
        protected readonly ApiContext _context;
        protected readonly IMapper _mapper;
        public HelloLogin_Service(ILogger<HelloLogin_Service> logger, ApiContext context, IMapper mapper)
            : base(logger)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ISvcResponseBaseDto<HelloLogin_ResponseDto>> ExecuteAsync(HelloLogin_RequestDto requestDto)
        {
            var res = new SvcResponseDto<HelloLogin_ResponseDto>();
            var user = await _context.GetUserByName(requestDto.UserName);
            if (user == null)
            {
                res.Error(SvcCodeEnum.AccountLoginFailed, "Invalid account or password.");
                return res;
            }

            //var data = _mapper.Map<HelloLogin_ResponseDto>(user); // Missing type map configuration or unsupported mapping.
            var data = _mapper.Map<HelloLogin_ResponseModel>(user);
            return res.Success().SetData(new HelloLogin_ResponseDto
            {
                UserId = data.UserId,
                UserName = data.UserName,
            });
        }
    }
}
