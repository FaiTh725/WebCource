using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Commands.Group.CreateGroup;
using Test.Application.Queries.Group.GetById;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator mediator;

        public GroupController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddGroup(CreateGroupCommand request)
        {
            var groupId = await mediator.Send(request);

            var group = await mediator.Send(new GetGroupByIdQuery 
            { 
                Id = groupId
            });

            return Ok(group);
        }
    }
}
