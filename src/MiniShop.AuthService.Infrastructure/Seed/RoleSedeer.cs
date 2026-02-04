using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Domain.Enums;

namespace MiniShop.AuthService.Infrastructure.Seed
{
    public static class RoleSedeer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(UserRole)))
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole<Guid>(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
