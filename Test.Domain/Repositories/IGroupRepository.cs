using CSharpFunctionalExtensions;
using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<StudentGroup?> GetGroup(int groupName);

        Task<StudentGroup?> GetGroup(long id);

        Task<StudentGroup> AddGroup(StudentGroup group);
    }
}
