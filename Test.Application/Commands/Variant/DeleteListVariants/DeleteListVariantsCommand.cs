
using MediatR;

namespace Test.Application.Commands.Variant.DeleteListVariants
{
    public class DeleteListVariantsCommand : IRequest
    {
        public long QuestionId { get; set; }

        public List<long> VariantsId { get; set; } = new List<long>();
    }
}
