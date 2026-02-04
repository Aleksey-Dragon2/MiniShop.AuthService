using Microsoft.IdentityModel.Tokens;
using MiniShop.AuthService.Application.Abstractions.TokenGenerator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniShop.AuthService.Infrastructure.Implementations.TokenGenerator
{
    public class TokenService : ITokenService
    {
        public Task<string> GenerateServiceTokenAsync(string secretKey, string issuer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creditals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: new[]
                {
                    new Claim("service", "AuthService"),
                },
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creditals);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
