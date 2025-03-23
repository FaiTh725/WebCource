using Authorize.Application.Commands.AccessToken.RefreshToken;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using Authorize.Application.Queries.User.GetUserByAccessToken;
using Authorize.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authorize.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtService<GenerateUserToken, TokenResponse> jwtService;

        public RefreshTokenController(
            IMediator mediator,
            IJwtService<GenerateUserToken, TokenResponse> jwtService)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Refresh()
        {
            var oldRefreshToken = Request.Cookies["refresh_token"];

            var newRefreshToken = await mediator.Send(new RefreshTokenCommand
            {
                RefreshToken = oldRefreshToken ?? ""
            });

            var ownerRefreshToken = await mediator.Send(new GetUserByAccessTokenQuery
            {
                AccessToken = newRefreshToken
            });

            var accessToken = jwtService.GenerateToken(new GenerateUserToken
            {
                Email = ownerRefreshToken.Email,
                RoleName = ownerRefreshToken.RoleName,
            });

            Response.Cookies.Append("token", accessToken);
            Response.Cookies.Append("refresh_token", newRefreshToken);

            return Ok();
        }
    }
}
