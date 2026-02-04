using MiniShop.AuthService.Domain.Entities;

namespace MiniShop.AuthService.Application.Abstractions.TokenGenerator
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
