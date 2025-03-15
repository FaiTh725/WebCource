using Test.Application.Contracts.Question;
using Test.Application.Contracts.QuestionVariant;

namespace Test.Application.Contracts.TestAnswer
{
    public class TestAnswerResponse
    {
        public long Id { get; set; }

        public bool IsCorrect { get; set; }

        public required StudentQuestionResponse Question { get; set; }

        public List<QuestionVariantResponse> Answers { get; set; } = new List<QuestionVariantResponse>();
    }
}
