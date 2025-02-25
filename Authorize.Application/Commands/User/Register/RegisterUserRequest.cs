using MediatR;

namespace Authorize.Application.Commands.User.Register
{
    public class RegisterUserRequest : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Group { get; set; }
    }
}
