using MediatR;

namespace Authorize.Application.Commands.AccessToken.RefreshToken
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
