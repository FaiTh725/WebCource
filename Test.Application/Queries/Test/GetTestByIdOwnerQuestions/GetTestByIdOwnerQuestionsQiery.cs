using MediatR;
using Test.Application.Contracts.Question;

namespace Test.Application.Queries.Test.GetTestByIdOwnerQuestions
{
    public class GetTestByIdOwnerQuestionsQiery : IRequest<QuestionResponse>
    {
        public long Id { get; set; }
    }
}
