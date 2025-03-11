using MediatR;
using Test.Application.Contracts.Question;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Test.GetTestByIdOwnerQuestions
{
    public class GetTestByIdOwnerQuestionsHandler : IRequestHandler<GetTestByIdOwnerQuestionsQiery, QuestionResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTestByIdOwnerQuestionsHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QuestionResponse> Handle(GetTestByIdOwnerQuestionsQiery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
