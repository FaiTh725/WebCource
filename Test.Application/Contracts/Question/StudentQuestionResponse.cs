namespace Test.Application.Contracts.Question
{
    public class StudentQuestionResponse
    {
        public long Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public List<string> UrlImages { get; set; } = new List<string>();
    }
}
