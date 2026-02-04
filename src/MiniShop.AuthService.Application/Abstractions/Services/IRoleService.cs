using MiniShop.AuthService.Domain.Enums;
using MiniShop.AuthService.Domain.Entities;
namespace MiniShop.AuthService.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task AssignRoleToUserAsync(User user, UserRole role);
    }
}
