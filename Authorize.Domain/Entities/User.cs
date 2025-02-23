using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Authorize.Domain.Entities
{
    public class User : Entity
    {
        public const int MAX_PASSWORD_LENGTH = 30;
        public const int MIN_PASSWORD_LENGTH = 5;

        public string Email { get; init; } = string.Empty;
        
        public string PasswordHash {  get; private set; } = string.Empty;

        public Role Role { get; private set; }

        // For EF
        public User() { }
    
        protected User(
            string email,
            string passwordHash,
            Role role)
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public static bool IsValidPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if(password.Length > MAX_PASSWORD_LENGTH ||
                password.Length < MIN_PASSWORD_LENGTH)
            {
                return false;
            }

            if(!passwordRegex.IsMatch(password))
            {
                return false;
            }

            return true;
        }

        public static Result<User> Initialize(
            string email,
            string passwordHash,
            Role role)
        {
            if(string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(passwordHash))
            {
                return Result
                    .Failure<User>("Email or passwordhash is empty");
            }

            if(!emailRegex.IsMatch(email))
            {
                return Result
                    .Failure<User>("Email doesnt contains one letter and one number");
            }

            if(role is null)
            {
                return Result.Failure<User>("Role is null");
            }

            return Result.Success(new User(
                email,
                passwordHash,
                role));
        }

        protected static Regex passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d).+$");

        protected static Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
