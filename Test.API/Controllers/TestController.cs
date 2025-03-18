using Application.Shared.Exceptions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.API.Contacts.Test;
using Test.Application.Commands.Test.CreateTest;
using Test.Application.Commands.Test.StartTest;
using Test.Application.Commands.Test.StopTest;
using Test.Application.Commands.TestAccess.CloseAccess;
using Test.Application.Commands.TestAccess.OpenTest;
using Test.Application.Contracts.Teacher;
using Test.Application.Interfaces;
using Test.Application.Queries.Test.GetTestByIdFull;
using Test.Application.Queries.Test.GetTestByIdOwner;
using Test.Application.Queries.TestAttempt.GetTestAttemptByIdWithAnswers;
using Test.Domain.Event;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITokenService<TeacherToken> tokenService;
        private readonly IBus bus;

        public TestController(
            IMediator mediator,
            ITokenService<TeacherToken> tokenService,
            IBus bus)
        {
            this.mediator = mediator;
            this.tokenService = tokenService;
            this.bus = bus;
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
                TestId = request.TestId
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

        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> OpenTest(
            OpenTestCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }

        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> CloseTest(
            CloseTestCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }
    }
}
