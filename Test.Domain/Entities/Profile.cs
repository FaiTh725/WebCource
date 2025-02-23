using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Test.Domain.Entities
{
    public abstract class Profile : Entity
    {
        public const int MAX_NAME_LENGTH = 50;

        public string Email { get; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        protected Profile(string email, string name)
        {
            Email = email;
            Name = name;
        }

        protected static Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
