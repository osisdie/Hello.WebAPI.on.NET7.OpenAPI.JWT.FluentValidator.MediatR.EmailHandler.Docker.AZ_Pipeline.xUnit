using System.ComponentModel.DataAnnotations;

namespace Hello.MediatR.Domain.Contract
{
    public class HelloMediatRSvcConfig
    {
        public const string ServiceName = "hello-mediatr-api-debug";
        public const string DeployName = "hello-mediatr-api";
    }

    public enum HelloMediatRSvcCodeEnum
    {
        [Display(Name = "unset")]
        UnSet = 1000,

        UnAuthenticated = 1001,
        UnAuthorized = 1002,

        ErrorCode_Max = 2000
    }

    public class HelloMediatRSvcConst
    {
        
    }
}
