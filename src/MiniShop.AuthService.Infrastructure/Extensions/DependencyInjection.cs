using Microsoft.Extensions.DependencyInjection;
using MiniShop.AuthService.Application.Abstractions.Services;
using MiniShop.AuthService.Application.Abstractions.TokenGenerator;
using MiniShop.AuthService.Infrastructure.Implementations.TokenGenerator;
using MiniShop.AuthService.Infrastructure.Services;
namespace MiniShop.AuthService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
