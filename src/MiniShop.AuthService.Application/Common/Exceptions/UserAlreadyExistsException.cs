
using MiniShop.AuthService.Application.Common.Exceptions;

namespace MiniShop.AuthService.Domain.Exceptions
{
    public class UserAlreadyExistsException(string email)
        : BadRequestException(message: $"User with email {email} already exists");
}
