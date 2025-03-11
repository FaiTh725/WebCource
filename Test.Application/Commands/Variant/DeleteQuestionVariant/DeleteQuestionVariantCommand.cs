using MediatR;

namespace Test.Application.Commands.Variant.DeleteQuestionVariant
{
    public class DeleteQuestionVariantCommand : IRequest
    {
        public long QuestionVariantId { get; set; }
    }
}
