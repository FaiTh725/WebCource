using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext context;

        public TeacherRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Teacher> AddTeacher(Teacher teacher)
        {
            var teacherEntity = await context.Teachers
                .AddAsync(teacher);

            return teacherEntity.Entity;
        }

        public async Task DeleteTeacher(string email)
        {
            await context.Teachers
                .Where(x => x.Email == email)
                .ExecuteDeleteAsync();
        }

        public async Task<Teacher?> GetTeacher(string email)
        {
            var teacher = await context.Teachers
                .FirstOrDefaultAsync(x => x.Email == email);

            return teacher;
        }

        public async Task<Teacher?> GetTeacher(long id)
        {
            var teacher = await context.Teachers
                .FirstOrDefaultAsync(x => x.Id == id);

            return teacher;
        }
    }
}
