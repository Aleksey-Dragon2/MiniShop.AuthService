using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MiniShop.AuthService.Application.Abstractions.TokenGenerator;
using MiniShop.AuthService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MiniShop.AuthService.Infrastructure.Implementations.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly IOptions<AuthOptions> _options;
        public readonly UserManager<User> _userManager;
        public TokenGenerator(IOptions<AuthOptions> options, UserManager<User> userManager)
        {
            _options = options;
            _userManager = userManager;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(_options.Value.ExpirationTime),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
