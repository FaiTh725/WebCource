namespace Test.API.Contacts.QuestionVariant
{
    public class CreateQuestionVariantRequest
    {
        public long TestQuestionId { get; set; }

        public string Text { get; set; } = string.Empty;

        public IFormFileCollection? VariantImg { get; set; }

        public bool IsCorrect { get; set; }
    }
}
