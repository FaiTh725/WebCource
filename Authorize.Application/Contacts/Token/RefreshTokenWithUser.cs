namespace Authorize.Application.Contacts.Token
{
    public class RefreshTokenWithUser
    {
        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
