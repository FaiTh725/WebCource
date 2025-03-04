namespace Authorize.Application.Commands.User.Test.Events
{
    public class RegisterUserCompletedEvent
    {
        public string Email { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
