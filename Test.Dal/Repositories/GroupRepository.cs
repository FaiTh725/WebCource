using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext context;

        public GroupRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<StudentGroup> AddGroup(StudentGroup group)
        {
            var groupEntity = await context.Groups
                .AddAsync(group);

            return groupEntity.Entity;
        }

        public async Task<StudentGroup?> GetGroup(int groupName)
        {
            var group = await context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GroupName == groupName);

            return group;
        }

        public async Task<StudentGroup?> GetGroup(long id)
        {
            var group = await context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return group;
        }
    }
}
