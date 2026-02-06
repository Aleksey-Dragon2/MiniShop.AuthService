using System.Security.Authentication;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Domain.Entities;
using MiniShop.AuthService.Application.Abstractions.TokenGenerator;

namespace MiniShop.AuthService.Application.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(IMapper mapper, SignInManager<User> signInManager, ITokenGenerator tokenGenerator)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new AuthenticationException($"User with email {request.Email} not found");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
                throw new AuthenticationException("Invalid password or email");

            return await _tokenGenerator.GenerateTokenAsync(user);
        }
    }
}
