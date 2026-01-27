using Microsoft.AspNetCore.Identity;

namespace MiniShop.AuthService.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User() { }         
        public User(string email, string userName)
        {
            Id = Guid.NewGuid();
            Email = email;
            UserName = userName; 
            CreatedAt = DateTime.UtcNow;
        }
    }
}
