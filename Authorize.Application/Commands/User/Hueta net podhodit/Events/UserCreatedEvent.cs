namespace Authorize.Application.Commands.User.Test.Events
{
    public class UserCreatedEvent
    {
        public Guid CorrelationId { get; set; }

        public long UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Group { get; set; }
    }
}
