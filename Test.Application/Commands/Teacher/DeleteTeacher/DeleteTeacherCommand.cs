using MediatR;

namespace Test.Application.Commands.Teacher.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest
    {
        public string TeacherEmail { get; set; } = string.Empty; 
    }
}
