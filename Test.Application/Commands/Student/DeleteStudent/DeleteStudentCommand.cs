using MediatR;

namespace Test.Application.Commands.Student.DeleteStudent
{
    public class DeleteStudentCommand : IRequest
    {
        public string StudentEmail { get; set; } = string.Empty;
    }
}
