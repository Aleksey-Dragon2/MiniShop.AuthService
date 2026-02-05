
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniShop.AuthService.Application.Extensions;
using MiniShop.AuthService.Domain.Entities;
using MiniShop.AuthService.Infrastructure.Database;
using MiniShop.AuthService.Infrastructure.Extensions;
using MiniShop.AuthService.Infrastructure.Implementations.TokenGenerator;
using MiniShop.AuthService.Infrastructure.Seed;
using MiniShop.AuthService.API.Extensions;
namespace MiniShop.AuthService.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAplication()
                .AddInfrastructure(builder.Configuration)
                .AddPresentation(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                await RoleSedeer.SeedAsync(roleManager);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
