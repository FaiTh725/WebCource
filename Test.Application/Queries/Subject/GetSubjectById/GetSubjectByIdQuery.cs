using MediatR;
using Test.Application.Contracts.Subject;

namespace Test.Application.Queries.Subject.GetSubjectById
{
    public class GetSubjectByIdQuery : IRequest<SubjectResponse>
    {
        public long Id { get; set; }
    }
}
