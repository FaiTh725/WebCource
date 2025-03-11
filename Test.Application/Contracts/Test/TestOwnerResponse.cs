
using Test.Application.Contracts.Teacher;

namespace Test.Application.Contracts.Test
{
    public class TestOwnerResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public required TeacherResponse Owner { get; set; }
    }
}
