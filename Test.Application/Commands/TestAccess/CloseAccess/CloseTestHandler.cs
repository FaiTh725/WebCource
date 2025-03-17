using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Repositories;

namespace Test.Application.Commands.TestAccess.CloseAccess
{
    public class CloseTestHandler : IRequestHandler<CloseTestCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CloseTestHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CloseTestCommand request, CancellationToken cancellationToken)
        {
            var deleteTest = request.ListStudentId
                .Select(x => unitOfWork.TestAccessRepository
                    .DeleteTestAccess(request.TestId, x));

            await Task.WhenAll(deleteTest);
        }
    }
}
