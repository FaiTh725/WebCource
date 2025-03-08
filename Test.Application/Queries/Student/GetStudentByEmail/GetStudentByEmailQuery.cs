using MediatR;
using Test.Application.Contracts.Student;

namespace Test.Application.Queries.Student.GetStudentByEmail
{
    public class GetStudentByEmailQuery : IRequest<StudentResponse>
    {
        public string Email { get; set; } = string.Empty;
    }
}
