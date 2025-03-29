using MediatR;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;

namespace Test.Application.Queries.TestAttempt.GetTestAttemptByIdWithAnswers
{
    public class GetTestAttemptByIdWithAnswersQuery : 
        IRequest<TestAttemptWithAnswersResponse>, 
        IAccessQuery, 
        ICachQuery
    {
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public long AttemptId { get; set; }

        public string Key => "TestAttempts:" + AttemptId;

        public int ExpirationSecond => 120;
    }
}
