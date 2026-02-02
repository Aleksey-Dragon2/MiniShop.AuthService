using FluentValidation;
using MediatR;
using MiniShop.AuthService.Application.Users.Register;

namespace MiniShop.AuthService.Application.Users.Login
{
    public class LoginUserCommandValidation : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidation()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
