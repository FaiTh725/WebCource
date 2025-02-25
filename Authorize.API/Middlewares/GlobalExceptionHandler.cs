using Application.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Authorize.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IProblemDetailsService problemDetailsService;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IHostApplicationLifetime appLifetime,
            IProblemDetailsService problemDetailsService)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if(exception is AppConfigurationException appConf)
            {
                logger.LogError("Error configure application" +
                    " section with error - " + appConf.SectionWithError);

                appLifetime.StopApplication();
            }

            httpContext.Response.StatusCode = exception switch 
            { 
                NotFoundApiException => StatusCodes.Status404NotFound,
                BadRequestApiException => StatusCodes.Status400BadRequest,
                ConflictApiException => StatusCodes.Status409Conflict,
                InternalServerApiException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext 
            { 
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "Error iccured",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                }
            });

        }
    }
}
