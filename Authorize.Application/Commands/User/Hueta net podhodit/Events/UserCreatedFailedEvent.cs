namespace Authorize.Application.Commands.User.Test.Events
{
    public class UserCreatedFailedEvent
    {
        public Guid CorrelationId { get; set; }

        public long UserId { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
