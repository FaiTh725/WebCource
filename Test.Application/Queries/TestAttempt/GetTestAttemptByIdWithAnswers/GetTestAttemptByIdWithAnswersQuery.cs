using MediatR;
using Test.Application.Contracts.TestAttempt;

namespace Test.Application.Queries.TestAttempt.GetTestAttemptByIdWithAnswers
{
    public class GetTestAttemptByIdWithAnswersQuery : IRequest<TestAttemptWithAnswersResponse>
    {
        public long Id { get; set; }
    }
}
