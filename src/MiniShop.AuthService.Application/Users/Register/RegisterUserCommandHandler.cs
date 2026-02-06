using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniShop.AuthService.Application.Users.Results;
using MiniShop.AuthService.Application.Abstractions.Services;
using MiniShop.AuthService.Domain.Entities;
using MiniShop.AuthService.Domain.Enums;
using MiniShop.AuthService.Domain.Exceptions;

namespace MiniShop.AuthService.Application.Users.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(IMapper mapper, IRoleService roleService, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleService = roleService;
        }

        public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new ArgumentNullException(nameof(request));

            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new UserAlreadyExistsException(request.Email);

            var user = new User(request.Email, request.UserName);
            var result = await _userManager.CreateAsync(user, request.Password);
            
            await _roleService.AssignRoleToUserAsync(user, UserRole.Customer);

            return new RegisterUserResult(
                 Id: user.Id,
                 UserName: user.UserName,
                 Email: user.Email
             );
        }
    }
}
