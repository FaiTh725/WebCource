using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext context;

        public StudentRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var studentEntity = await context.Students
                .AddAsync(student);

            return studentEntity.Entity;
        }

        public async Task<Result<Student>> GetStudent(string email)
        {
            var student = await context.Students
                .FirstOrDefaultAsync(x => x.Email == email);

            if(student is null)
            {
                return Result.Failure<Student>("Student doesnt found");
            }

            return student;
        }
    }
}
