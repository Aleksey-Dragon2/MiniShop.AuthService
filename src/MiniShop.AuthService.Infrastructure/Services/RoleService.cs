using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Application.Abstractions.Services;
using MiniShop.AuthService.Domain.Entities;
using MiniShop.AuthService.Domain.Enums;
namespace MiniShop.AuthService.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> _userManager;
        public RoleService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task AssignRoleToUserAsync(User user, UserRole role)
        {
            var roleName = role.ToString();
            if (!await _userManager.IsInRoleAsync(user, roleName))
                await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
