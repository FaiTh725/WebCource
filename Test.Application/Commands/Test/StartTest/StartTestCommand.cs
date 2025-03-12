using MediatR;

namespace Test.Application.Commands.Test.StartTest
{
    public class StartTestCommand : IRequest<Guid>
    {
        public string StudentEmail { get; set; } = string.Empty;
        
        public long TestId { get; set; }

        public int TestTime { get; set; }
    }
}
