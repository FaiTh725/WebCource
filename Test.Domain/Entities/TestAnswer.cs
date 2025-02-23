using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class TestAnswer : Entity
    {
        public TestAttempt TestAttempt { get; init; }

        public TestVariant Answer { get; init; }

        public bool IsCorrect { get; init; }

        // For EF
        public TestAnswer(){}

        private TestAnswer(
            TestAttempt testAttempt,
            TestVariant answer,
            bool isCorrect)
        {
            TestAttempt = testAttempt;
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public static Result<TestAnswer> Initialize(
            TestAttempt testAttempt,
            TestVariant answer,
            bool isCorrect)
        {
            if(testAttempt is null ||
                answer is null)
            {
                return Result.Failure<TestAnswer>("Attempt or answer is null");
            }

            return Result.Success(new TestAnswer(
                testAttempt,
                answer,
                isCorrect));
        }
    }
}
