using CSharpFunctionalExtensions;
using Test.Application.Contracts.Test;
using Test.Application.Contracts.TestAnswer;
using Test.Application.Contracts.TestAttempt;
using Test.Domain.Entities;

namespace Test.Application.Interfaces
{
    public interface ITestEvaluationService
    {
        public Result<SolvedTestResult> CalculateTestResult(
                    AttemptRedisEntity attempt, 
                    List<TestQuestion> testQuestions,
                    TestAttempt testAttempt);
    }
}
