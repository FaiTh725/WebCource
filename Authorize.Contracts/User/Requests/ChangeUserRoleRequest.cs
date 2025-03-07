namespace Authorize.Contracts.User.Requests
{
    public class ChangeUserRoleRequest
    {
        public string Email { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
