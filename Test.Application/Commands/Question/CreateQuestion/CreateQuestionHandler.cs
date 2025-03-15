using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.File;
using Test.Application.Interfaces;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Question.CreateQuestion
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public CreateQuestionHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task<long> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var test = await unitOfWork.TestRepository
                .GetTest(request.TestId);

            if (test is null)
            {
                throw new BadRequestApiException("Test does not exist");
            }

            var questionEntity = TestQuestion.Initialize(
                test,
                request.Question,
                request.Type,
                request.QuestionWeight);

            if(questionEntity.IsFailure)
            {
                throw new BadRequestApiException("Bad request - " +
                    questionEntity.Error);
            }

            await unitOfWork.BeginTransactionAsync();

            var newQuestion = await unitOfWork.QuestionRepository
                .AddQuestion(questionEntity.Value);

            //var newVariants = new List<TestVariant>();
            var variantsDictinary = new Dictionary<TestVariant, List<FileEntity>>();

            foreach(var variant in request.Variants)
            {
                var variantEntity = TestVariant.Initialize(
                    variant.Text,
                    newQuestion,
                    variant.IsCorrect);

                if (variantEntity.IsFailure)
                {
                    await unitOfWork.RollBackTransactionAsync();
                    throw new BadRequestApiException("Incorrect variant question - " +
                        variantEntity.Error);
                }

                var newVariant = await unitOfWork.QuestionVariantRepository
                    .AddQuestionVariant(variantEntity.Value);

                variantsDictinary.Add(newVariant, variant.VariantImages);
                //newVariants.Add(newVariant);
            }

            var addVariantsResult = newQuestion
                .TestQuestionForValid();

            if(addVariantsResult.IsFailure)
            {
                await unitOfWork.RollBackTransactionAsync();
                throw new BadRequestApiException(addVariantsResult.Error);
            }

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommiTransactionAsync();

            var uploadImagesTasks = new List<Task<string>>();

            foreach(var variant in variantsDictinary)
            {
                uploadImagesTasks.AddRange(
                    variant.Value.Select(x => blobService.UploadBlob(
                        Path.Combine(variant.Key.ImageFolder, Guid.NewGuid().ToString()),
                        x.Stream,
                        x.ContentType)));
            }

            var uploadQuestionImagesTasks = request.QuestionImages
                    .Select(x => blobService.UploadBlob(
                        Path.Combine(newQuestion.ImageFolder, Guid.NewGuid().ToString()),
                        x.Stream,
                        x.ContentType,
                        cancellationToken))
                    .ToList();

            uploadImagesTasks.AddRange(uploadQuestionImagesTasks);

            await Task.WhenAll(uploadImagesTasks);

            return newQuestion.Id;
        }
    }
}
