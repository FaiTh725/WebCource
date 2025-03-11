using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Subject;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Subject.GetSubjectById
{
    public class GetSubjectByIdHandler : IRequestHandler<GetSubjectByIdQuery, SubjectResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectByIdHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<SubjectResponse> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await unitOfWork
                .SubjectRepository.GetSubject(request.Id);

            if(subject is null)
            {
                throw new NotFoundApiException("Subject doesnt not exist");
            }

            return new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }
    }
}
