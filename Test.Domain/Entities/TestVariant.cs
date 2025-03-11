using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class TestVariant : Entity
    {
        public string ImageFolder { get => $"Variant-{Id}"; }

        public string Text { get; init; }

        public bool IsCorrect { get; private set; }

        public TestQuestion Question { get; init; }

        // For EF
        public TestVariant() {}

        private TestVariant(
            string text,
            TestQuestion question,
            bool isCorrect = false)
        {
            Text = text;
            IsCorrect = isCorrect;
            Question = question;
        }

        public static Result<TestVariant> Initialize(
            string text,
            TestQuestion question,
            bool isCorrect = false)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return Result.Failure<TestVariant>("Text is null or white space");
            }

            if (question is null)
            {
                return Result.Failure<TestVariant>("Test variant must contains question that it will be contains");
            }

            return Result.Success(new TestVariant(
                text,
                question,
                isCorrect));
        }
    }
}
