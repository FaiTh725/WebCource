using MediatR;

namespace Authorize.Application.Commands.AccessToken.CreateRefreshToken
{
    public class CreateRefreshTokenCommand : IRequest<string>
    {
        public long UserId { get; set; }
    }
}
