using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class Student : Profile
    {
        public StudentGroup Group { get; private set; }

        public Student() : base("", ""){}

        private Student(
            string email,
            string name,
            StudentGroup group) : 
            base(email, name)
        {
            Group = group;
        }

        public static Result<Student> Initialize(
            string email,
            string name,
            StudentGroup group)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Student>("Email and Name is nul");
            }

            if (name.Length > MAX_NAME_LENGTH)
            {
                return Result.Failure<Student>("Name is too long max - " +
                    MAX_NAME_LENGTH.ToString());
            }

            if (!emailRegex.IsMatch(email))
            {
                return Result.Failure<Student>("Email should has @ and dot after @");
            }

            if(group is null)
            {
                return Result.Failure<Student>("Group is null");
            }

            return Result.Success(new Student(
                email, 
                name, 
                group));
        }
    }
}
