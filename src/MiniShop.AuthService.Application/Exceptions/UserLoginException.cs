using Microsoft.AspNetCore.Identity;

namespace MiniShop.AuthService.Application.Exceptions
{
    public class UserLoginException : Exception
    {
        public UserLoginException() : base("User login failed")
        { 
        }
    }
}
