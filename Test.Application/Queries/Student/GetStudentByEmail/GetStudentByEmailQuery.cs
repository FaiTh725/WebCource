using MediatR;
using Test.Application.Contracts.Student;
using Test.Application.Interfaces;

namespace Test.Application.Queries.Student.GetStudentByEmail
{
    public class GetStudentByEmailQuery : 
        IRequest<StudentResponse>,
        ICachQuery
    {
        public string Email { get; set; } = string.Empty;

        public string Key => "StudentByEmail:" + Email;

        public int ExpirationSecond => 120;
    }
}
