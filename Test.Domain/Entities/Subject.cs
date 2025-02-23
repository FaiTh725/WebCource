using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class Subject : Entity
    {
        public string Name { get; private set; } = string.Empty;

        public List<Test> Tests { get; } = new List<Test>();

        // For EF
        public Subject() {}

        private Subject(
            string name)
        {
            Name = name;
        }

        public static Result<Subject> Initialize(
            string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Subject>("Name is null");
            }

            return Result.Success(new Subject(
                name));
        }
    }
}
