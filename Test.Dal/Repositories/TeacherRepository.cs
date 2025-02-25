using CSharpFunctionalExtensions;
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

        public async Task<Result<Teacher>> GetTeacher(string email)
        {
            var teacher = await context.Teachers
                .FirstOrDefaultAsync(x => x.Email == email);

            if(teacher is null)
            {
                return Result.Failure<Teacher>("Teacher doesnt found");
            }

            return teacher;
        }
    }
}
