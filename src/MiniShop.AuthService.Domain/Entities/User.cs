namespace MiniShop.AuthService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public List<string> Roles { get; private set; } = new();

        public DateTime CreatedAt { get; private set; }

        public User(string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }

        public void AddRole(string role)
        {
            if (!Roles.Contains(role))
            {
                Roles.Add(role);
            }
        }

        public void RemoveRole(string role)
        {
            Roles.Remove(role);
        }
    }
}
