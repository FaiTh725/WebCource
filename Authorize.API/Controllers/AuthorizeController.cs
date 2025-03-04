using Authorize.Application.Commands.User.Login;
using Authorize.Application.Commands.User.Register;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using Authorize.Application.Queries.User.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authorize.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtService<GenerateUserToken, TokenResponse> jwtService;

        public AuthorizeController(
            IMediator mediator,
            IJwtService<GenerateUserToken, TokenResponse> jwtService)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login([FromQuery]LoginUserRequest request)
        {
            var userId = await mediator.Send(request);

            var user = await mediator.Send(new GetUserByIdQuery
            {
                Id = userId
            });

            var token = jwtService.GenerateToken(new GenerateUserToken
            {
                Email = user.Email,
                RoleName = user.RoleName
            });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("token", token);

            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var userId = await mediator.Send(request);

            var user = await mediator.Send(new GetUserByIdQuery
            {
                Id = userId
            });

            var token = jwtService.GenerateToken(new GenerateUserToken
            {
                Email = user.Email,
                RoleName = user.RoleName
            });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("token", token);

            return Ok(user);
        }
    }
}
