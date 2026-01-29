using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Runtime.CompilerServices;

namespace MiniShop.AuthService.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
            services.AddAutoMapper(cfg =>
            cfg.AddMaps(typeof(DependencyInjection).Assembly));

            return services;
        }

    }
}
