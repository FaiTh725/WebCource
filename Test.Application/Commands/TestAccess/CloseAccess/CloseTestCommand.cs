using MediatR;

namespace Test.Application.Commands.TestAccess.CloseAccess
{
    public class CloseTestCommand : IRequest
    {
        public long TestId { get; set; }

        public List<long> ListStudentId { get; set; } = new List<long>();
    }
}
