using MediatR;

namespace Authorize.Application.Commands.User.ChangeRole
{
    public class ChangeRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
