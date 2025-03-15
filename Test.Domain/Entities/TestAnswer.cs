using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class TestAnswer : Entity
    {
        public TestAttempt TestAttempt { get; init; }

        public List<TestVariant> Answers { get; init; }
        //public List<long> AnswersId { get; init; }

        public TestQuestion Question { get; init; }
        public long QuestionId { get; init; }


        public bool IsCorrect { get; init; }


        // For EF
        public TestAnswer(){}

        private TestAnswer(
            TestAttempt testAttempt,
            List<TestVariant> answers,
            TestQuestion question,
            bool isCorrect)
        {
            TestAttempt = testAttempt;
            Answers = answers;
            Question = question;
            IsCorrect = isCorrect;
        }

        //private TestAnswer(
        //    TestAttempt testAttempt,
        //    List<long> answersId,
        //    long questionId,
        //    bool isCorrect)
        //{
        //    TestAttempt = testAttempt;
        //    AnswersId = answersId;
        //    QuestionId = questionId;
        //    IsCorrect = isCorrect;
        //}

        //public static Result<TestAnswer> Initialize(
        //    TestAttempt testAttempt,
        //    List<long> answersId,
        //    long questionId,
        //    bool isCorrect)
        //{
        //    if (testAttempt is null)
        //    {
        //        return Result.Failure<TestAnswer>("Attemptis null");
        //    }

        //    return Result.Success(new TestAnswer(
        //        testAttempt,
        //        answersId,
        //        questionId,
        //        isCorrect));
        //}

        public static Result<TestAnswer> Initialize(
            TestAttempt testAttempt,
            List<TestVariant> answer,
            TestQuestion question,
            bool isCorrect)
        {
            if(testAttempt is null ||
                answer is null ||
                question is null)
            {
                return Result.Failure<TestAnswer>("Attempt, question or answer is null");
            }

            return Result.Success(new TestAnswer(
                testAttempt,
                answer,
                question,
                isCorrect));
        }
    }
}
