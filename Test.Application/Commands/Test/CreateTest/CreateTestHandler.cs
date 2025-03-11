using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Repositories;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Application.Commands.Test.CreateTest
{
    public class CreateTestHandler : IRequestHandler<CreateTestCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateTestHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var teacher = await unitOfWork.TeacherRepository
                .GetTeacher(request.CreatorEmail);

            if(teacher is null)
            {
                throw new BadRequestApiException("Teacher with entered email doesnt exist");
            }

            var subject = await unitOfWork.SubjectRepository
                .GetSubject(request.SibjectId);

            if(subject is null)
            {
                throw new BadRequestApiException("Subject with entered id doesnt exist");
            }

            var testEntity = TestEntity.Initialize(
                request.Name,
                subject,
                teacher);

            if (testEntity.IsFailure)
            {
                throw new BadRequestApiException("Invalid request field - " + 
                    testEntity.Error);
            }

            var newTest = await unitOfWork.TestRepository
                .AddTest(testEntity.Value);

            await unitOfWork.SaveChangesAsync();

            return newTest.Id;
        }
    }
}
