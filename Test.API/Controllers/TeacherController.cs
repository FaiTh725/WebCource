using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Commands.Teacher.CreateTeacher;
using Test.Application.Events.Teacher;
using Test.Application.Queries.Teacher.GetTeacherById;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IBus bus;

        public TeacherController(
            IMediator mediator,
            IBus bus)
        {
            this.mediator = mediator;
            this.bus = bus;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTeacher(CreateTeacherCommand request)
        {
            await bus.Publish(new CreateTeacherRequest
            {
                Email = request.Email
            });

            return Ok();

            //var teacherId = await mediator.Send(request);

            //var teacher = await mediator.Send(new GetTeacherByIdQuery 
            //{ 
            //    Id = teacherId
            //});

            //return Ok(teacher);
        }
    }
}
