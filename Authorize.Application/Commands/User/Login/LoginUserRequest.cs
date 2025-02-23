using MediatR;

namespace Authorize.Application.Commands.User.Login
{
    public class LoginUserRequest : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;

        public string Password {  get; set; } = string.Empty ;
    }
}
