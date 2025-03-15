using MediatR;
using Test.Application.Commands.Variant.CreateVariant;
using Test.Application.Contracts.File;
using Test.Application.Contracts.QuestionVariant;
using Test.Domain.Enums;

namespace Test.Application.Commands.Question.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<long>
    {
        public long TestId { get; set; }

        public string Question { get; set; } = string.Empty;

        public QuestionType Type { get; set; }

        public int QuestionWeight { get; set; }

        public List<VariantRequest> Variants { get; set; } = new List<VariantRequest>();

        public List<FileEntity> QuestionImages { get; set; } = new List<FileEntity>();
    }
}
