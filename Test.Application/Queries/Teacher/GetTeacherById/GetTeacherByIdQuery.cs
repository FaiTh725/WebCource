using MediatR;
using Test.Application.Contracts.Teacher;

namespace Test.Application.Queries.Teacher.GetTeacherById
{
    public class GetTeacherByIdQuery : IRequest<TeacherResponse>
    {
        public long Id { get; set; }
    }
}
