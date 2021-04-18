using CoreFX.Auth.Contracts.Login;
using Hello.MediatR.Domain.DataAccess.Entities;
using Hello.MediatR.Domain.SDK.Services.AuthServices.Login.Models;

namespace Hello.MediatR.Domain.SDK.Services.AuthServices.Login
{
    public class HelloLogin_Mapper : AutoMapper.Profile
    {
        public HelloLogin_Mapper()
        {
            CreateMap<UserEntity, HelloLogin_ResponseModel>()
                .ForMember(des => des.UserId, opt => { opt.MapFrom(map => map.Id); })
                .ForMember(des => des.UserName, opt => { opt.MapFrom(map => map.Name); })
                .ReverseMap();

            CreateMap<UserEntity, AuthLogin_ResponseDto>()
                .ForMember(des => des.UserId, opt => { opt.MapFrom(map => map.Id); })
                .ForMember(des => des.UserName, opt => { opt.MapFrom(map => map.Name); })
                .ReverseMap();
        }
    }
}
