using Authorize.Application.Contacts.User;
using MediatR;

namespace Authorize.Application.Queries.User.GetUserByAccessToken
{
    public class GetUserByAccessTokenQuery : IRequest<UserResponse>
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}
