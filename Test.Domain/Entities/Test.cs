using CSharpFunctionalExtensions;
using Test.Domain.Enums;

namespace Test.Domain.Entities
{
    public class Test : Entity
    {
        public string Name { get; private set; } = string.Empty;

        public Subject Subject { get; init; }

        public Teacher Owner { get; init; }

        public List<TestQuestion> Questions { get; init; } = new List<TestQuestion>();

        // For EF
        public Test() {}

        private Test(
            string name,
            Subject subject,
            Teacher owner)
        {
            Name = name;
            Subject = subject;
            Owner = owner;
        }

        public static Result<Test> Initialize(
            string name,
            Subject subject,
            Teacher owner)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Test>("Name is null");
            }

            if (subject is null ||
                owner is null)
            {
                return Result.Failure<Test>("Subject or Owner is null");
            }

            return Result.Success(new Test(
                name,
                subject,
                owner));
        }

    }
}
