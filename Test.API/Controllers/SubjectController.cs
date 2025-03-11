using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Commands.Subject.CreateSubject;
using Test.Application.Queries.Subject.GetSubjectById;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator mediator;

        public SubjectController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddSubject(CreateSubjectCommand request)
        {
            var subjectId = await mediator.Send(request);

            var subject = await mediator.Send(new GetSubjectByIdQuery
            {
                Id = subjectId
            });

            return Ok(subject);
        }
    }
}
