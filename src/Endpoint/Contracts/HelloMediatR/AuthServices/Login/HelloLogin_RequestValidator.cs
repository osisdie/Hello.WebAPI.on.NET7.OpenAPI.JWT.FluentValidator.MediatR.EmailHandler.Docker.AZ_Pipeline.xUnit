using FluentValidation;

namespace Hello.MediatR.Domain.Contract.AuthServices.Login
{
    public class HelloLogin_RequestValidator : AbstractValidator<HelloLogin_RequestDto>
    {
        public HelloLogin_RequestValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().Length(4, 255);
            RuleFor(c => c.Password).NotEmpty().Length(6, 255);
        }
    }
}
