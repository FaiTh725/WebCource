using CSharpFunctionalExtensions;
using Test.Domain.Enums;

namespace Test.Domain.Entities
{
    public class TestQuestion : Entity
    {
        public string ImageFolder { get => $"Question-{Id}"; }

        public Test Test { get; init; }

        public string Question { get; set; } = string.Empty;

        public List<TestVariant> Variants { get; init; } = new List<TestVariant>();

        public int QuestionWeight { get; set; }

        public QuestionType QuestionType { get; private set; }

        // For EF
        public TestQuestion() {}

        private TestQuestion(
            Test test,
            string question,
            QuestionType type = QuestionType.OneAnswer,
            int questionWeight = 1)
        {
            Test = test;
            Question = question;
            QuestionType = type;
            QuestionWeight = questionWeight;
        }

        public Result TestQuestionForValid()
        {
            var countCorrectVariants = Variants
                .Count(x => x.IsCorrect);
            
            if (countCorrectVariants > 1 &&
                QuestionType == QuestionType.OneAnswer)
            {
                return Result.Failure("Question with type OneAnswer should " +
                    "has one answer");
            }

            return Result.Success();
        }

        public static Result<TestQuestion> Initialize(
            Test test,
            string question,
            QuestionType type = QuestionType.OneAnswer,
            int questionWeight = 1)
        {
            if (test is null)
            {
                return Result.Failure<TestQuestion>("Question should has owner test");
            }

            if (string.IsNullOrWhiteSpace(question))
            {
                return Result.Failure<TestQuestion>("Question text cant be empty");
            }

            if(questionWeight < 1)
            {
                return Result.Failure<TestQuestion>("Question weight cant be less than 1");
            }

            return Result.Success(new TestQuestion(
                test,
                question,
                type,
                questionWeight));
        }
    }
}
