using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext context;

        public SubjectRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Subject> AddSubject(Subject subject)
        {
            var newSubject = await context.Subjects
                .AddAsync(subject);

            return newSubject.Entity;
        }

        public async Task<Subject?> GetSubject(long id)
        {
            return await context.Subjects
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Subject?> GetSubject(string name)
        {
            return await context.Subjects
                .FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
