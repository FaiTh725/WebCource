using Application.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.API.Contacts.Test;
using Test.Application.Commands.Test.CreateTest;
using Test.Application.Contracts.Teacher;
using Test.Application.Interfaces;
using Test.Application.Queries.Test.GetTestByIdOwner;

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
    }
}
