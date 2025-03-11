
namespace Test.API.Contacts.QuestionVariant
{
    public class CreateVariantQuestion
    {
        public string Text { get; set; } = string.Empty;

        public IFormFileCollection? VariantImg { get; set; }

        public bool IsCorrect { get; set; }
    }
}
