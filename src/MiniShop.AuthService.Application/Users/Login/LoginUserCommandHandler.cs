using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Application.Users.Results;
using MiniShop.AuthService.Application.Exceptions;
using MiniShop.AuthService.Domain.Entities;

namespace MiniShop.AuthService.Application.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public LoginUserCommandHandler(IMapper mapper, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
                throw new UserLoginException();
        }
    }
}
