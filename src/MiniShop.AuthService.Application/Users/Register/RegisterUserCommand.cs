using MediatR;
using MiniShop.AuthService.Application.Users.Results;

namespace MiniShop.AuthService.Application.Users.Register
{
    public record RegisterUserCommand (string UserName, string Email, string Password)
        :IRequest<RegisterUserResult>;
}
