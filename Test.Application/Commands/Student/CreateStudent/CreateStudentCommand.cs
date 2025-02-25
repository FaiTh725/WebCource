using MediatR;

namespace Test.Application.Commands.Student.CreateStudent
{
    public class CreateStudentCommand : IRequest<long>
    {
        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Group { get; set; }
    }
}
