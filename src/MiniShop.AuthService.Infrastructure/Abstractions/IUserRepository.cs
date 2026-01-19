
using MiniShop.AuthService.Domain.Entities;

namespace MiniShop.AuthService.Infrastructure.Abstractions
{
    public interface IUserRepository
    {
        public Task<User> AddAsync(User user);
        public Task<User> UpdateAsync(User user);
        public Task<User?> GetByEmailAsync(string email);
        public Task<bool> DeleteAsync(Guid id);
    }
}
