using MediatR;

namespace Test.Application.Commands.Teacher.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<long>
    {
        public string Email { get; set; } = string.Empty;
    }
}
