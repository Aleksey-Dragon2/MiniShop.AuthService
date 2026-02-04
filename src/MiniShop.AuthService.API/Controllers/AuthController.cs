using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniShop.AuthService.Application.Users.Results;
using MiniShop.AuthService.Application.Users.Register;
using MiniShop.AuthService.Application.Users.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniShop.AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserResult>> Register([FromBody] RegisterUserCommand request)
        {
            var user = await _mediator.Send(new RegisterUserCommand(
                request.UserName, request.Email, request.Password));

            return Ok(new RegisterUserResult(user.Id, user.UserName, user.Email));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserCommand request)
        {
            var token =  await _mediator.Send(new LoginUserCommand(
                request.Email, request.Password));
            return Ok(token);
        }
    }
}
