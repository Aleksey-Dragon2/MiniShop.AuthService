using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Application.Exceptions;
using MiniShop.AuthService.Application.Users.Results;
using MiniShop.AuthService.Domain.Entities;

namespace MiniShop.AuthService.Application.Users.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(IMapper mapper, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new ArgumentNullException(nameof(request));

            var user = new User(request.Email, request.UserName);
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                throw new UserRegistrationException(result.Errors);

            return _mapper.Map<RegisterUserResult>(result);
        }
    }
}
