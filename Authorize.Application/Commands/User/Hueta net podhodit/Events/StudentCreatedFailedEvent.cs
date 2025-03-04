namespace Authorize.Application.Commands.User.Test.Events
{
    public class StudentCreatedFailedEvent
    {
        public Guid CorrelationId { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
