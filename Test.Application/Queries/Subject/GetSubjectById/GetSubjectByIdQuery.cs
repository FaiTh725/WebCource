using MediatR;
using Test.Application.Contracts.Subject;
using Test.Application.Interfaces;

namespace Test.Application.Queries.Subject.GetSubjectById
{
    public class GetSubjectByIdQuery : 
        IRequest<SubjectResponse>,
        ICachQuery
    {
        public long Id { get; set; }

        public string Key => "Subjects:" + Id;

        public int ExpirationSecond => 240;
    }
}
