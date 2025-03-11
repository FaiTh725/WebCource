using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Repositories;
using SubjectEntity = Test.Domain.Entities.Subject;

namespace Test.Application.Commands.Subject.CreateSubject
{
    public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateSubjectHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var existSubject = await unitOfWork.SubjectRepository
                .GetSubject(request.Name);

            if (existSubject is not null)
            {
                throw new ConflictApiException("Subject Alerady Created");
            }

            var subject = SubjectEntity.Initialize(request.Name);

            if(subject.IsFailure)
            {
                throw new BadRequestApiException(subject.Error);
            }

            var newSubject = await unitOfWork
                .SubjectRepository.AddSubject(subject.Value);

            await unitOfWork.SaveChangesAsync();

            return newSubject.Id;
        }
    }
}
