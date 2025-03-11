using MediatR;

namespace Test.Application.Commands.Question.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest
    {
        public long QuestionId { get; set; }
    }
}
