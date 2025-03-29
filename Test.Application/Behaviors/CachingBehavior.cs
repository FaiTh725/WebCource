using MediatR;
using Test.Application.Interfaces;

namespace Test.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachQuery
    {
        private readonly ICachService cacheService;

        public CachingBehavior(
            ICachService cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var cacheValues = await cacheService.GetData<TResponse>(request.Key);

            if(cacheValues.IsSuccess)
            {
                return cacheValues.Value;
            }

            var response = await next();

            await cacheService.SetData(
                request.Key,
                response,
                request.ExpirationSecond);

            return response;
        }
    }
}
