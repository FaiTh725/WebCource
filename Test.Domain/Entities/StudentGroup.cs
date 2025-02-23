using CSharpFunctionalExtensions;

namespace Test.Domain.Entities
{
    public class StudentGroup : Entity
    {
        public const int GROUP_LENGTH = 8;

        public int GroupName { get; set; }

        public List<Student> Students { get; init; }

        // For EF
        public StudentGroup(){}

        private StudentGroup(
            int groupName)
        {
            GroupName = groupName;

            Students = new List<Student>();
        }

        public static Result<StudentGroup> Initialize(
            int groupName)
        {
            if (groupName.ToString().Length != GROUP_LENGTH)
            {
                return Result.Failure<StudentGroup>("Group should has 8 digits");
            }

            return Result.Success(new StudentGroup(
                groupName));
        }
    }
}
