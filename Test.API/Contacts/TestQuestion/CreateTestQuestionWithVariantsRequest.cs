using Test.API.Contacts.QuestionVariant;
using Test.Domain.Enums;

namespace Test.API.Contacts.TestQuestion
{
    public class CreateTestQuestionWithVariantsRequest
    {
        public long TestId { get; set; }

        public string Question { get; set; } = string.Empty;

        public IFormFileCollection? QuestionImg { get; set; }

        public QuestionType Type { get; set; } = QuestionType.OneAnswer;

        public List<CreateVariantQuestion> Variants { get; set; } = new List<CreateVariantQuestion>();
    }
}
