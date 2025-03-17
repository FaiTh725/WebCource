using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Common.Interfaces;
using Test.Application.Interfaces;

namespace Test.Application.Behaviors
{
    public class TestAccessBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITestAccessRequest
    {
        private readonly ITestAccessService testAccessService;

        public TestAccessBehaviour(
            ITestAccessService testAccessService)
        {
            this.testAccessService = testAccessService;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if(await testAccessService.HasAccess(request.TestId, request.StudentEmail))
            {
                throw new ForbiddenAccessApiException("Test is close");
            }

            return await next();
        }
    }
}
