using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.TestAnswer;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Domain.Enums;
using Test.Domain.Repositories;

namespace Test.Application.Commands.QuestionAnswer
{
    public class AddQuestionAnswerHandler : IRequestHandler<AddQuestionAnswerCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRedisEntityService<AttemptRedisEntity> attemptRedisService;

        public AddQuestionAnswerHandler(
            IUnitOfWork unitOfWork,
            IRedisEntityService<AttemptRedisEntity> attemptRedisService)
        {
            this.unitOfWork = unitOfWork;
            this.attemptRedisService = attemptRedisService;
        }

        public async Task Handle(AddQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var testSession = await attemptRedisService
                .GetEntity(request.TestSessionId);

            if(testSession is null)
            {
                throw new BadRequestApiException("Current sessnion doesnt exist");
            }

            var question = await unitOfWork.QuestionRepository
                .GetQuestionWithVariants(request.QuestionId);

            var requestVariantIsValid = question is null ? false : 
                request.TestVariantsId.ToHashSet().IsSubsetOf(question.Variants.Select(x => x.Id));

            if (!requestVariantIsValid)
            {
                throw new BadRequestApiException("Question doesnt exist or invalid variants");
            }

            if(!(question!.QuestionType == QuestionType.OneAnswer &&
                request.TestVariantsId.Count == 1))
            {
                throw new BadRequestApiException("Question has only one answer");
            }

            testSession.Answers.Add(new AnswerRedisEntity
            {
                QuestionId = request.QuestionId,
                SendTime = DateTime.UtcNow,
                TestAnswersId = request.TestVariantsId
            });

            await attemptRedisService.UpdateEntity(testSession);
        }
    }
}
