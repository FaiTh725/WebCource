namespace Test.Application.Contracts.QuestionVariant
{
    public class QuestionVariantResponse
    {
        public long QuestionVariantId { get; set; }

        public List<string> VariantImages { get; set; } = new List<string>();

        public string Text { get; set; } = string.Empty;


    }
}
