using MediatR;

namespace Test.Application.Commands.Group.CreateGroup
{
    public class CreateGroupCommand : IRequest<long>
    {
        public int Name { get; set; }
    }
}
