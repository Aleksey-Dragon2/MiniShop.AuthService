using Microsoft.AspNetCore.Identity;

namespace MiniShop.AuthService.Application.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public IReadOnlyCollection<IdentityError> Errors { get; }

        public UserRegistrationException(IEnumerable<IdentityError> errors)
            : base("User registration failed")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}
