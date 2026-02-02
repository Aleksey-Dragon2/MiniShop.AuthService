using MiniShop.AuthService.Domain.Entities;
using MiniShop.AuthService.Infrastructure.Abstractions;
using MiniShop.AuthService.Infrastructure.Database;

namespace MiniShop.AuthService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;
        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            user.PasswordHash = "";
            return user;
        }
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            user.PasswordHash = "";
            return user;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FindAsync(email);
            if (user != null)
            {
                user.PasswordHash = "";
            }
            return user;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
