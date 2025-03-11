using MediatR;

namespace Test.Application.Commands.Test.CreateTest
{
    public class CreateTestCommand : IRequest<long>
    {
        public string CreatorEmail { get; set; } = string.Empty;

        public long SibjectId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
