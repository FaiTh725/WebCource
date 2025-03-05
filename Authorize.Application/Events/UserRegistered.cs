namespace Authorize.Application.Events
{
    public class UserRegistered
    {
        public Guid CorrelationId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
