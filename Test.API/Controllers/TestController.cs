using Application.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.API.Contacts.Test;
using Test.Application.Commands.Test.CreateTest;
using Test.Application.Commands.Test.StartTest;
using Test.Application.Commands.Test.StopTest;
using Test.Application.Contracts.Teacher;
using Test.Application.Interfaces;
using Test.Application.Queries.Test.GetTestByIdFull;
using Test.Application.Queries.Test.GetTestByIdOwner;
using Test.Application.Queries.TestAttempt.GetTestAttemptByIdWithAnswers;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITokenService<TeacherToken> tokenService;

        public TestController(
            IMediator mediator,
            ITokenService<TeacherToken> tokenService)
        {
            this.mediator = mediator;
            this.tokenService = tokenService;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateTest(CreateTestRequest request)
        {
            var token = Request.Cookies["token"] ??
                throw new InternalServerApiException("Cookie doesnt has token");

            var decodeToken = tokenService.DecodeToken(token);

            if(decodeToken.IsFailure)
            {
                throw new InternalServerApiException("Invalid authorize token");
            }

            var testId = await mediator.Send(new CreateTestCommand
            {
                CreatorEmail = decodeToken.Value.Email,
                Name = request.Name,
                SibjectId = request.SibjectId
            });

            var test = await mediator.Send(new GetTestByIdOwnerQuery
            {
                Id = testId
            });

            return Ok(test);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> StartTest(StartTestRequest request)
        {
            var token = Request.Cookies["token"] ??
                throw new InternalServerApiException("Cookie doesnt has token");

            var decodeToken = tokenService.DecodeToken(token);

            if(decodeToken.IsFailure)
            {
                throw new InternalServerApiException("Invalid authorize token");
            }

            var attemptId = await mediator.Send(new StartTestCommand
            {
                StudentEmail = decodeToken.Value.Email,
                TestId = request.TestId,
                TestTime = request.TestTime
            });

            var test = await mediator.Send(new GetTestByIdFullQuery
            {
                TestId = request.TestId
            });

            return Ok(new StartTestResponse
            {
                Test = test,
                AttemptId = attemptId
            });
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> StopTest(
            StopTestCommand request)
        {
            var testResultId = await mediator.Send(request);

            return Ok(testResultId);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllTest(
            [FromQuery] GetTestByIdFullQuery query)
        {
            var test = await mediator.Send(query);

            return Ok(test);
        }

        // TODO Only Teacher, Admin and Owner Attempt Can get the TestResult
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetTestResult(
            [FromQuery] long attemptId)
        {
            var token = Request.Cookies["token"] ??
                throw new InternalServerApiException("Cookie doesnt has token");

            var decodeToken = tokenService.DecodeToken(token);

            if (decodeToken.IsFailure)
            {
                throw new InternalServerApiException("Invalid authorize token");
            }

            var result = await mediator.Send(new GetTestAttemptByIdWithAnswersQuery
            {
                Email = decodeToken.Value.Email,
                Role = decodeToken.Value.Role,
                AttemptId = attemptId
            });

            return Ok(result);
        }
    }
}
