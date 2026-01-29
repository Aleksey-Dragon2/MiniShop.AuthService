using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniShop.AuthService.Application.Users.Results;
using Microsoft.AspNetCore.Identity.Data;
using MiniShop.AuthService.Application.Users.Register;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniShop.AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthConroller : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthConroller(IMediator mediator)
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
    }
}
