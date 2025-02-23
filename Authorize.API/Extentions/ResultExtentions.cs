using ControllerResult = Microsoft.AspNetCore.Http.IResult;
using MethodResult = CSharpFunctionalExtensions.Result;

namespace Authorize.API.Extentions
{
    // TODO: delete if it is useless
    public static class ResultExtentions
    {
        public static ControllerResult ToBadRequestProblemDetail(
            this MethodResult result)
        {
            if(result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Results.Problem(
                type: "Bad Request",
                title: "Invalid units",
                detail: result.Error,
                statusCode: StatusCodes.Status400BadRequest);
        }

        public static ControllerResult ToNotFoundProblemDetail(
            this MethodResult result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Results.Problem(
                type: "Not Found",
                title: "Not found values",
                detail: result.Error,
                statusCode: StatusCodes.Status404NotFound);
        }

        public static ControllerResult ToInternalServerProblemDetail(
            this MethodResult result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Results.Problem(
                type: "Internal Server Error",
                title: "Unknown Server Error",
                detail: result.Error,
                statusCode: StatusCodes.Status500InternalServerError);
        }

        public static ControllerResult ToConflictProblemDetail(
            this MethodResult result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Results.Problem(
                type: "Conflict",
                title: "Conflict",
                detail: result.Error,
                statusCode: StatusCodes.Status409Conflict);
        }
    }
}
