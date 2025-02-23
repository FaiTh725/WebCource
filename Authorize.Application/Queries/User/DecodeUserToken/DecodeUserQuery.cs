using Authorize.Application.Contacts.Token;
using MediatR;

namespace Authorize.Application.Queries.User.DecodeUserToken
{
    public class DecodeUserQuery : IRequest<TokenResponse>
    {
        public string Token { get; set; } = string.Empty;
    }
}
