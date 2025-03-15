﻿using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.TestAttempt;
using Test.Application.Interfaces;
using Test.Application.Queries.Test.Specifications;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Test.StopTest
{
    public class StopTestHandler : IRequestHandler<StopTestCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITestEvaluationService testEvaluationService;
        private readonly IRedisEntityService<AttemptRedisEntity> attemptRegisService;

        public StopTestHandler(
            IUnitOfWork unitOfWork,
            ITestEvaluationService testEvaluationService,
            IRedisEntityService<AttemptRedisEntity> attemptRegisService)
        {
            this.unitOfWork = unitOfWork;
            this.testEvaluationService = testEvaluationService;
            this.attemptRegisService = attemptRegisService;
        }

        public async Task<long> Handle(StopTestCommand request, CancellationToken cancellationToken)
        {
            var attempt = await attemptRegisService
                .GetEntity(request.AttemptId);
        
            if(attempt is null)
            {
                throw new BadRequestApiException("Attempt doesnt exist");
            }

            // как то долго
            var student = await unitOfWork.StudentRepository
                .GetStudent(attempt.AnswerStudentId);

            if(student is null)
            {
                throw new InternalServerApiException("Student attempt doesnt exist");
            }

            var test = unitOfWork.TestRepository
                .GetTests(new TestByIdWithQuestionsAndAnswersSpecification(attempt.TestId))
                .FirstOrDefault();

            if (test is null)
            {
                throw new InternalServerApiException("Test attempt doesnt exist");
            }

            var testAttempt = TestAttempt.Initialize(
                student,
                test,
                new List<TestAnswer>(),
                0);

            if (testAttempt.IsFailure)
            {
                throw new InternalServerApiException("Unknown Error Save Result");
            }

            var testResult = testEvaluationService.CalculateTestResult(
                attempt, test.Questions, testAttempt.Value);

            if(testResult.IsFailure)
            {
                throw new InternalServerApiException(testResult.Error);
            }

            testAttempt.Value.Percent = Math.Truncate((double)testResult.Value.Percent);
            testAttempt.Value.Answers.AddRange(testResult.Value.StudentAnswers);

            var testAttemptAdded = await unitOfWork.TestAttemptRepository
                .AddTestTestAttempt(testAttempt.Value);

            await unitOfWork.SaveChangesAsync();

            // Delete attempt
            //await attemptRegisService.RemoveEntity(attempt.AttemptId);

            return testAttemptAdded.Id;
        }
    }
}
