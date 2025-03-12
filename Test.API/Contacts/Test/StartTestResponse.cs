using Test.Application.Contracts.Test;

namespace Test.API.Contacts.Test
{
    public class StartTestResponse
    {
        public Guid AttemptId { get; set; }

        public required TestFullResponse Test { get; set; }
    }
}
