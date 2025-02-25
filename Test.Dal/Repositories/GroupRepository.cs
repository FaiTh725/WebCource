using CSharpFunctionalExtensions;
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
            var groupEntity = await context.Groups.AddAsync(group);

            return groupEntity.Entity;
        }

        public async Task<Result<StudentGroup>> GetGroup(int groupName)
        {
            var group = await context.Groups
                .FirstOrDefaultAsync(x => x.GroupName == groupName);

            if(group is null)
            {
                return Result.Failure<StudentGroup>("Group doesnt Found");
            }

            return group;
        }

        public async Task<Result<StudentGroup>> GetGroup(long id)
        {
            var group = await context.Groups
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null)
            {
                return Result.Failure<StudentGroup>("Group doesnt Found");
            }

            return group;
        }
    }
}
