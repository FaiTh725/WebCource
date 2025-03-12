using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Question;
using Test.Application.Contracts.QuestionVariant;
using Test.Application.Contracts.Test;
using Test.Application.Interfaces;
using Test.Application.Queries.Test.Specifications;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Test.GetTestByIdFull
{
    public class GetTestByIdFullHandler :
        IRequestHandler<GetTestByIdFullQuery, TestFullResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public GetTestByIdFullHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task<TestFullResponse> Handle(GetTestByIdFullQuery request, CancellationToken cancellationToken)
        {
            var test = unitOfWork.TestRepository
                .GetTests(new TestByIdWithQuestionsAndAnswersSpecification(request.TestId))
                .FirstOrDefault();

            if (test is null)
            {
                throw new NotFoundApiException("Test doesnt exist");
            }

            var questionsResponse = await Task.WhenAll(test.Questions
                .Select(x => GetQuestionResponse(x)));

            return new TestFullResponse
            {
                Id = test.Id,
                Name = test.Name,
                Questions = questionsResponse.ToList()
            };
        }

        private async Task<QuestionResponse> GetQuestionResponse(TestQuestion testQuestion)
        {
            return new QuestionResponse
            {
                Id = testQuestion.Id,
                Text = testQuestion.Question,
                Type = testQuestion.QuestionType,
                UrlImages = await blobService.GetBlobFolder(testQuestion.ImageFolder),
                QuestionVariants = (await Task.WhenAll(testQuestion.Variants
                        .Select(x => GetQuestionVariantResponse(x))))
                    .ToList()
            };
        }

        private async Task<QuestionVariantResponse> GetQuestionVariantResponse(
            TestVariant variant)
        {
            return new QuestionVariantResponse
            {
                QuestionVariantId = variant.Id,
                Text = variant.Text,
                VariantImages = await blobService.GetBlobFolder(variant.ImageFolder)
            };
        }
    }
}
