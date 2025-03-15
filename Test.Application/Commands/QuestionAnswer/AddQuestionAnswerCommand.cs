using MediatR;

namespace Test.Application.Commands.QuestionAnswer
{
    public class AddQuestionAnswerCommand : IRequest
    {
        public Guid TestSessionId { get; set; }

        public List<long> TestVariantsId { get; set; } = new List<long>();

        public long QuestionId { get; set; }
    }
}
