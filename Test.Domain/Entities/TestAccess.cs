
using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class TestAccess
    {
        public long TestId { get; set; }
        public Test Test { get; set; }

        public long StudentId { get; set; }
        public Student Student { get; set; }

        public int TestDuration { get; set; }

        // For EF
        public TestAccess(){}

        private TestAccess(
            Test test,
            Student student,
            int testDuration)
        {
            Test = test;
            Student = student;
            TestDuration = testDuration;
        }

        public static Result<TestAccess> Initialize(
            Test test,
            Student student,
            int testDuration)
        {
            if(test is null ||
                student is null)
            {
                return Result.Failure<TestAccess>("Test And Student is required");
            }

            if(testDuration <= 0)
            {
                return Result.Failure<TestAccess>("Test Duration should be greater than zero");
            }

            return Result.Success(new TestAccess(
                test, student, testDuration));
        }
    }
}
