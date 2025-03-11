using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Interfaces;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Question.DeleteQuestion
{
    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public DeleteQuestionHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await unitOfWork.QuestionRepository
                .GetQuestionWithVariants(request.QuestionId);

            if(question is null)
            {
                throw new NotFoundApiException("Question doesnt exist");
            }

            var deleteImagesTasks = new List<Task>();

             deleteImagesTasks.AddRange(
                 question.Variants
                 .Select(x => blobService
                    .DeleteBlobFolder(
                     x.ImageFolder, 
                     cancellationToken))
                 .ToList());

            deleteImagesTasks.Add(blobService
                .DeleteBlobFolder(
                    question.ImageFolder,
                    cancellationToken));

            await Task.WhenAll(deleteImagesTasks);

            await unitOfWork.QuestionRepository
                .DeleteQuestion(request.QuestionId);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
