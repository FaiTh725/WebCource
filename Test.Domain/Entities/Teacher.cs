using CSharpFunctionalExtensions;
using Test.Domain.Event;

namespace Test.Domain.Entities
{
    public class Teacher : Profile
    {
        public List<Test> CreatedTests { get; } = new List<Test>();

        // For EF
        public Teacher() :base("", ""){}

        private Teacher(
            string email,
            string name):
            base(email, name)
        {
            //RaiseDomainEvent(new TeacherRegisteredEvent 
            //{ 
            //    Email = email
            //});
        }

        public static Result<Teacher> Initialize(
            string email,
            string name)
        {
            if(string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Teacher>("Email and Name is nul");
            }

            if(name.Length > MAX_NAME_LENGTH)
            {
                return Result.Failure<Teacher>("Name is too long max - " + 
                    MAX_NAME_LENGTH.ToString());
            }

            if(!emailRegex.IsMatch(email))
            {
                return Result.Failure<Teacher>("Email should has @ and dot after @");
            }

            return Result.Success(new Teacher(
                email,
                name));
        }
    }
}
