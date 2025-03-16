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

        public async Task DeleteStudent(string email)
        {
            await context.Students
                .Where(x => x.Email == email)
                .ExecuteDeleteAsync();
        }

        public async Task<Student?> GetStudent(string email)
        {
            var student = await context.Students
                .FirstOrDefaultAsync(x => x.Email == email);

            return student;
        }

        public async Task<Student?> GetStudent(long id)
        {
            return await context.Students
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student?> GetStudentWithGroup(string email)
        {
            var student = await context.Students
                .Where(x => x.Email == email)
                .Include(x => x.Group)
                .FirstOrDefaultAsync();

            return student;
        }
    }
}
