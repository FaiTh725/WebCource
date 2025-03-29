using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Commands.Teacher.CreateTeacher;
using Test.Application.Events.Teacher;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IBus bus;

        public TeacherController(
            IBus bus)
        {
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
        }
    }
}
