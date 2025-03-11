using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Interfaces;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Variant.DeleteQuestionVariant
{
    public class DeleteQuestionVariantHandler :
        IRequestHandler<DeleteQuestionVariantCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public DeleteQuestionVariantHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task Handle(DeleteQuestionVariantCommand request, CancellationToken cancellationToken)
        {
            var questionVariant = await unitOfWork
                .QuestionVariantRepository
                .GetQuestionVariant(request.QuestionVariantId);

            if(questionVariant is null)
            {
                throw new NotFoundApiException("Test variant doesn not exist");
            }

            await unitOfWork.QuestionVariantRepository
                .DeleteQuestionVariant(request.QuestionVariantId);

            await blobService.DeleteBlobFolder(
                questionVariant.ImageFolder, 
                cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
