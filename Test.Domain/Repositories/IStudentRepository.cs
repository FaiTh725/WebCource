
using CSharpFunctionalExtensions;
using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudent(string email);

        Task<Student?> GetStudent(long id);

        Task<Student?> GetStudentWithGroup(string email);

        Task<Student> AddStudent(Student student);

        Task DeleteStudent(string email);
    }
}
