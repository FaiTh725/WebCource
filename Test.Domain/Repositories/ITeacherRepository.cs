using CSharpFunctionalExtensions;
using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface ITeacherRepository
    {
        Task<Result<Teacher>> GetTeacher(string email);

        Task<Teacher> AddTeacher(Teacher teacher);
    }
}
