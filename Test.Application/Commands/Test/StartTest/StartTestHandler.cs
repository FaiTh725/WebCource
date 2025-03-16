using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Commands.Test.StopTest;
using Test.Application.Common.Mediator;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Test.StartTest
{
    public class StartTestHandler : IRequestHandler<StartTestCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;
        private IRedisEntityService<AttemptRedisEntity> attemptRedisService;
        private readonly IBackgroundJobService backgroundJobService;

        public StartTestHandler(
            IRedisEntityService<AttemptRedisEntity> attemptRedisService,
            IUnitOfWork unitOfWork,
            IBackgroundJobService backgroundJobService)
        {
            this.attemptRedisService = attemptRedisService;
            this.unitOfWork = unitOfWork;
            this.backgroundJobService = backgroundJobService;
        }

        public async Task<Guid> Handle(StartTestCommand request, CancellationToken cancellationToken)
        {
            var student = await unitOfWork.StudentRepository
                .GetStudent(request.StudentEmail);

            if (student is null)
            {
                throw new InternalServerApiException("Request from unknown account");
            }

            var test = await unitOfWork.TestRepository
                .GetTest(request.TestId);

            if(test is null)
            {
                throw new NotFoundApiException("Test doesnt exist");
            }

            var attemptStarted = await attemptRedisService
                .GetEntity(student.Id);

            if(attemptStarted is not null)
            {
                throw new ConflictApiException("Test has already started");
            }

            var testAttemptId = Guid.NewGuid();

            var cancelTestJobId = backgroundJobService.CreateDelaydedJob<MediatorWrapper>(x =>
            x.SendCommand(new StopTestCommand
            {
                AttemptId = testAttemptId
            }),
            TimeSpan.FromSeconds(request.TestTime));

            var testAttempts = new AttemptRedisEntity
            {
                AttemptId = testAttemptId,
                AnswerStudentId = student.Id,
                TestId = test.Id,
                TestTime = request.TestTime,
                JobId = cancelTestJobId,
                StartDate = DateTime.UtcNow
            };

            await attemptRedisService.AddEntity(testAttempts);


            return testAttempts.AttemptId;
        }
    }
}
