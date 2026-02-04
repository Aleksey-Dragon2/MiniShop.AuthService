using MediatR;

namespace MiniShop.AuthService.Application.Users.Login
{
    public record LoginUserCommand(string Email, string Password) : IRequest<string>;
}
