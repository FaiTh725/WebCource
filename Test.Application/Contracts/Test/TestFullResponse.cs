using Test.Application.Contracts.Question;

namespace Test.Application.Contracts.Test
{
    public class TestFullResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<QuestionResponse> Questions { get; set; } = new List<QuestionResponse>();
    }
}
