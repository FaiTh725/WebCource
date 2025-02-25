using Authorize.Application.Commands.User.Login;
using Authorize.Application.Commands.User.Register;
using Authorize.Application.Queries.User.DecodeUserToken;
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

        public AuthorizeController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login([FromQuery]LoginUserRequest request)
        {
            var token = await mediator.Send(request);

            var decodeToken = await mediator.Send(new DecodeUserQuery
            {
                Token = token
            });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("token", token);

            return Ok(decodeToken);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var token = await mediator.Send(request);

            var decodeToken = await mediator.Send(new DecodeUserQuery
            {
                Token = token
            });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            
            Response.Cookies.Append("token", token);

            return Ok(decodeToken);
        }
    }
}
