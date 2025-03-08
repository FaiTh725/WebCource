using CSharpFunctionalExtensions;
using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetTeacher(string email);

        Task<Teacher?> GetTeacher(long id);

        Task<Teacher> AddTeacher(Teacher teacher);

        Task DeleteTeacher(string email);
    }
}
