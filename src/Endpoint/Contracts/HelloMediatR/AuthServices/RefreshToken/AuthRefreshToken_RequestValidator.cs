using FluentValidation;
using CoreFX.Auth.Contracts.RefreshToken;

namespace Hello.MediatR.Domain.Contract.AuthServices.RefreshToken
{
    public class AuthRefreshToken_RequestValidator : AbstractValidator<AuthRefreshToken_RequestDto>
    {
        public AuthRefreshToken_RequestValidator()
        {
            RuleFor(c => c.RefreshToken).NotEmpty();
        }
    }
}
