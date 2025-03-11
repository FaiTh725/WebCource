using MediatR;
using Test.Application.Contracts.File;

namespace Test.Application.Commands.Variant.CreateVariant
{
    public class CreateVariantCommand : IRequest<long>
    {
        public long QuestionId { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public List<FileEntity> VariantImages { get; set; } = new();
    }
}
