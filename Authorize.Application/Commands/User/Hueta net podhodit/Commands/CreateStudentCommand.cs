namespace Authorize.Application.Commands.User.Test.Commands
{
    public class CreateStudentCommand
    {
        public Guid CorrelationId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Group { get; set; }
    }
}
