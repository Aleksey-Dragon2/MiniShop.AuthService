
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Infrastructure.Database.AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services
                .AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.Configure<AuthOptions>(
                builder.Configuration.GetSection("AuthOptions"));


            builder.Services.AddAplication()
                .AddInfrastructure()
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
