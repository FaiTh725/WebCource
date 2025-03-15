using CSharpFunctionalExtensions;
using Test.Application.Contracts.Test;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Application.Queries.TestVariant.Specifications;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Application.Implementations
{
    public class TestEvaluationService : ITestEvaluationService
    {
        private readonly IUnitOfWork unitOfWork;

        public TestEvaluationService(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Result<SolvedTestResult> CalculateTestResult(
            AttemptRedisEntity attempt, 
            List<TestQuestion> testQuestions,
            TestAttempt testAttempt)
        {
            var studentPoints = 0;
            var maxPoints = 0;
            var userAnswers = new List<TestAnswer>();

            foreach (var question in testQuestions)
            {
                maxPoints += question.QuestionWeight;

                // last student answer by this question
                var studentAnswer = attempt.Answers
                    .Where(x => x.QuestionId == question.Id)
                    .OrderByDescending(x => x.SendTime)
                    .FirstOrDefault();

                if (studentAnswer is null)
                {
                    continue;
                }

                // Get id correct question answers
                var rightAnswersId = question.Variants
                        .Where(x => x.IsCorrect)
                        .Select(x => x.Id)
                        .ToList();

                // Check that two list has the same values
                var answersIsMath = studentAnswer.TestAnswersId
                    .ToHashSet().IsSubsetOf(rightAnswersId);

                if (answersIsMath)
                {
                    studentPoints += question.QuestionWeight;
                }

                var userAnswersList = unitOfWork.QuestionVariantRepository
                    .GetQuestionVariants(new GetVariantsByListId(studentAnswer.TestAnswersId))
                    .ToList();

                var testAnswer = TestAnswer.Initialize(
                    testAttempt, userAnswersList, 
                    question, answersIsMath);

                if (testAnswer.IsFailure)
                {
                    return Result.Failure<SolvedTestResult>("Error with initialize Test Answer Entity");
                }

                userAnswers.Add(testAnswer.Value);
            }

            return Result.Success(new SolvedTestResult
            {
                Percent = Math.Truncate((double)studentPoints / maxPoints * 10000) / 100,
                StudentAnswers = userAnswers
            });
        }
    }
}
