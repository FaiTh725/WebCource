using CSharpFunctionalExtensions;
using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<Result<StudentGroup>> GetGroup(int groupName);

        Task<Result<StudentGroup>> GetGroup(long id);

        Task<StudentGroup> AddGroup(StudentGroup group);
    }
}
