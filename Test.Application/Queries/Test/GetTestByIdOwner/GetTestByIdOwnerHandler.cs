using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Teacher;
using Test.Application.Contracts.Test;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Test.GetTestByIdOwner
{
    public class GetTestByIdOwnerHandler : IRequestHandler<GetTestByIdOwnerQuery, TestOwnerResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTestByIdOwnerHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TestOwnerResponse> Handle(GetTestByIdOwnerQuery request, CancellationToken cancellationToken)
        {
            var testWithOwner = await unitOfWork.TestRepository
                .GetTestWithOwner(request.Id);

            if (testWithOwner is null)
            {
                throw new NotFoundApiException("Test with current id doesnt exist");
            }

            return new TestOwnerResponse
            {
                Id = testWithOwner.Id,
                Name = testWithOwner.Name,
                Owner = new TeacherResponse
                {
                    Id = testWithOwner.Owner.Id,
                    Email = testWithOwner.Owner.Email,
                    Name = testWithOwner.Owner.Name
                }
            };
        }
    }
}
