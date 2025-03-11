using Application.Shared.Exceptions;
using CSharpFunctionalExtensions;
using MediatR;
using Test.Application.Interfaces;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Variant.CreateVariant
{
    
    public class CreateVariantHandler : IRequestHandler<CreateVariantCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public CreateVariantHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task<long> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
        {
            var question = await unitOfWork.QuestionRepository
                .GetQuestionWithVariants(request.QuestionId);

            if (question is null)
            {
                throw new BadRequestApiException("Question doesnt not exist");
            }

            var questionVariant = TestVariant.Initialize(
                request.Text,
                question,
                request.IsCorrect);

            if(questionVariant.IsFailure)
            {
                throw new BadRequestApiException("Error request - " + 
                    questionVariant.Error);
            }

            await unitOfWork.BeginTransactionAsync();

            var newVariant = await unitOfWork.QuestionVariantRepository
                .AddQuestionVariant(questionVariant.Value);

            var validQuestionResult = question
                .TestQuestionForValid();

            if (validQuestionResult.IsFailure)
            {
                await unitOfWork.RollBackTransactionAsync();
                throw new ConflictApiException(validQuestionResult.Error);
            }

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommiTransactionAsync();

            var uploadFilesTasks = request.VariantImages.Select(x =>
                blobService.UploadBlob(
                    Path.Combine(newVariant.ImageFolder, Guid.NewGuid().ToString()),
                     x.Stream,
                     x.ContentType,
                     cancellationToken)
            ).ToList();

            await Task.WhenAll(uploadFilesTasks);

            return newVariant.Id;
        }
    }
}
