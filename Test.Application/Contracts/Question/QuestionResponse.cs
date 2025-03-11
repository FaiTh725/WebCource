using Test.Domain.Enums;

namespace Test.Application.Contracts.Question
{
    public class QuestionResponse
    {
        public long Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public List<string> UrlImages { get; set; } = new List<string>();

        public QuestionType Type { get; set; }
    }
}
