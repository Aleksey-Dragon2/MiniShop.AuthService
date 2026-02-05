using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniShop.AuthService.Application.Abstractions.Services;
using MiniShop.AuthService.Application.Abstractions.TokenGenerator;
using MiniShop.AuthService.Domain.Entities;
using MiniShop.AuthService.Infrastructure.Database;
using MiniShop.AuthService.Infrastructure.Implementations.TokenGenerator;
using MiniShop.AuthService.Infrastructure.Services;

namespace MiniShop.AuthService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(connectionString));
            services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
