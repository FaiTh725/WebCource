using Authorize.Domain.Entities;
using Authorize.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Authorize.Dal.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext context;

        public RoleRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Role> AddRole(Role role)
        {
            var roleEntity = await context.Roles.AddAsync(role);

            return roleEntity.Entity;
        }

        public async Task<Result<Role>> GetRole(string roleName)
        {
            var role = await context.Roles
                .FirstOrDefaultAsync(x => x.RoleName == roleName);

            if (role is null)
            {
                return Result.Failure<Role>("Role doesnt found");
            }

            return Result.Success(role);
        }

        public IQueryable<Role> GetRoles()
        {
            return context.Roles;
        }
    }
}
