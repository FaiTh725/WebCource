using CSharpFunctionalExtensions;

namespace Authorize.Domain.Entities
{
    public class Role
    {
        public string RoleName { get; init; } = string.Empty;
    
        public List<User> Users { get; init; } = new List<User>();
        
        // For EF
        public Role() { }

        protected Role(
            string roleName)
        {
            RoleName = roleName;
        }

        public static Result<Role> Initialize(
            string roleName)
        {
            if(string.IsNullOrWhiteSpace(roleName))
            {
                return Result.Failure<Role>("Role name is empty or null");
            }

            return Result.Success(new Role(
                roleName));
        }
    }
}
