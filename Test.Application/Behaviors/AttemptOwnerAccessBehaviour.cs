using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Interfaces;
using Test.Domain.Repositories;

namespace Test.Application.Behaviors
{
    public class AttemptOwnerAccessBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IAccessQuery
    {
        private readonly IUnitOfWork unitOfWork;

        public AttemptOwnerAccessBehaviour(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var attempt = await unitOfWork.TestAttemptRepository
                .GetTestAttemptWithOwner(request.AttemptId);


            if( attempt is null ||
                (request.Role == "User" && 
                 attempt.AnswerStudent.Email != request.Email))
            {
                throw new ForbiddenAccessApiException("Only owner has access");
            }
            else
            {
                return await next();
            }
        }
    }
}
