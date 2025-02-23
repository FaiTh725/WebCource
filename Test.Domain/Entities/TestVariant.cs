using CSharpFunctionalExtensions;
using System.Globalization;

namespace Test.Domain.Entities
{
    public class TestVariant : Entity
    {
        public string ImageFolder { get => $"Variant-{Id}"; }

        public string Text { get; init; }

        public bool IsCorrect { get; private set; }

        public Test Test { get; init; }

        // For EF
        public TestVariant() {}

        private TestVariant(
            string text,
            Test test,
            bool isCorrect = false)
        {
            Text = text;
            IsCorrect = isCorrect;
            Test = test;
        }

        public static Result<TestVariant> Initialize(
            string text,
            Test test,
            bool isCorrect = false)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return Result.Failure<TestVariant>("Text is null or white space");
            }

            return Result.Success(new TestVariant(
                text, 
                test,
                isCorrect));
        }
    }
}
