using Test.Application.Contracts.File;

namespace Test.Application.Contracts.QuestionVariant
{
    public class VariantRequest
    {
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public List<FileEntity> VariantImages { get; set; } = new();
    }
}
