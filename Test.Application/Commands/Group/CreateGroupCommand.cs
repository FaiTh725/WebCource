using MediatR;

namespace Test.Application.Commands.Group
{
    public class CreateGroupCommand : IRequest<long>
    {
        public int Name { get; set; }
    }
}
