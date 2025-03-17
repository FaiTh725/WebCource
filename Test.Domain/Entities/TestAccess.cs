
using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class TestAccess
    {
        public long TestId { get; set; }
        public Test Test { get; set; }

        public long StudentId { get; set; }
        public Student Student { get; set; }

        // For EF
        public TestAccess(){}

        private TestAccess(
            Test test,
            Student student)
        {
            Test = test;
            Student = student;
        }

        public static Result<TestAccess> Initialize(
            Test test,
            Student student)
        {
            if(test is null ||
                student is null)
            {
                return Result.Failure<TestAccess>("Test And Student is required");
            }

            return Result.Success(new TestAccess(
                test, student));
        }
    }
}
