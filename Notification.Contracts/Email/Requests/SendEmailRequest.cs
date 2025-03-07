namespace Notification.Contracts.Email.Requests
{
    public class SendEmailRequest
    {
        public required string Consumer { get; init; }

        public string Message { get; set; } = string.Empty;

        public required string Subject { get; init; }
    }
}
