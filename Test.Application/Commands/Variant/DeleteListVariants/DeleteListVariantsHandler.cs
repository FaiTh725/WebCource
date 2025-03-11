using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Interfaces;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Variant.DeleteListVariants
{
    public class DeleteListVariantsHandler :
        IRequestHandler<DeleteListVariantsCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public DeleteListVariantsHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task Handle(DeleteListVariantsCommand request, CancellationToken cancellationToken)
        {
            var question = await unitOfWork.QuestionRepository
                .GetQuestionWithVariants(request.QuestionId);

            if (question is null)
            {
                throw new NotFoundApiException("Question doesnt exist");
            }

            var deleteImageTasks = new List<Task>();
            var deleteIdList = new List<long>();

            foreach(var variantId in request.VariantsId)
            {
                var variant = question.Variants
                            .FirstOrDefault(x => x.Id == variantId);
                
                if (variant is not null)
                {
                    deleteImageTasks.Add(
                        blobService.DeleteBlobFolder(
                            variant.ImageFolder, 
                            cancellationToken));

                    deleteIdList.Add(variantId);
                }
                else
                {
                    throw new BadRequestApiException(
                        $"Question variant with id {variantId} doesnt exist");
                }
            }

            await Task.WhenAll(deleteImageTasks);

            await unitOfWork.QuestionVariantRepository
                .DeleteQuestionVariants(deleteIdList);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
