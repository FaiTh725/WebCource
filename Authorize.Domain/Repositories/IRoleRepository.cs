using Authorize.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Authorize.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRole(string roleName);

        Task<Role> AddRole(Role role);

        IQueryable<Role> GetRoles();
    }
}
