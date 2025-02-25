
using CSharpFunctionalExtensions;
using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<Result<Student>> GetStudent(string email);

        Task<Student> AddStudent(Student student);
    }
}
