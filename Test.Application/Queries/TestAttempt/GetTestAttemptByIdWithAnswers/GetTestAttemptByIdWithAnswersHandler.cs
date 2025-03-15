using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Question;
using Test.Application.Contracts.QuestionVariant;
using Test.Application.Contracts.TestAnswer;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Application.Queries.TestAttempt.Specifications;
using Test.Domain.Repositories;

namespace Test.Application.Queries.TestAttempt.GetTestAttemptByIdWithAnswers
{
    public class GetTestAttemptByIdWithAnswersHandler :
        IRequestHandler<GetTestAttemptByIdWithAnswersQuery, TestAttemptWithAnswersResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBlobService blobService;

        public GetTestAttemptByIdWithAnswersHandler(
            IUnitOfWork unitOfWork,
            IBlobService blobService)
        {
            this.unitOfWork = unitOfWork;
            this.blobService = blobService;
        }

        public async Task<TestAttemptWithAnswersResponse> Handle(GetTestAttemptByIdWithAnswersQuery request, CancellationToken cancellationToken)
        {
            var attempt = unitOfWork.TestAttemptRepository
                .GetTestAttempts(new AttemptByIdWithAnswersAndQuestion(request.Id))
                .FirstOrDefault();

            if(attempt is null)
            {
                throw new NotFoundApiException("Attempt doesnt exist");
            }

            var studentAnswers = new List<TestAnswerResponse>();

            foreach(var answer in attempt.Answers)
            {
                // Change
                var questionVariantsTasks = answer.Answers.Select(async x => new QuestionVariantResponse
                {
                    Text = x.Text,
                    QuestionVariantId = x.Id,
                    VariantImages = await blobService.GetBlobFolder(x.ImageFolder)
                }).ToList();

                var questionVariants = await Task.WhenAll(questionVariantsTasks);

                studentAnswers.Add(new TestAnswerResponse
                {
                    Question = new StudentQuestionResponse
                    {
                        Id = answer.Question.Id,
                        Text = answer.Question.Question,
                        UrlImages = await blobService
                            .GetBlobFolder(answer.Question.ImageFolder, cancellationToken)
                    },
                    Id = answer.Id,
                    IsCorrect = answer.IsCorrect,
                    Answers = questionVariants.ToList()
                });
            }

            var attemptResponse = new TestAttemptWithAnswersResponse
            {
                Id = attempt.Id,
                PassedTime = attempt.AnswerDate,
                Percent = attempt.Percent,
                Answers = studentAnswers
            };

            return attemptResponse;
        }
    }
}
