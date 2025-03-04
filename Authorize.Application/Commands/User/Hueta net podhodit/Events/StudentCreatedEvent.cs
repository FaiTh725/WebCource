namespace Authorize.Application.Commands.User.Test.Events
{
    public class StudentCreatedEvent
    {
        public Guid CorrelationId { get; set; }

        public long StudentId { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
