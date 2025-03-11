using Test.Domain.Entities;

namespace Test.Domain.Repositories
{
    public interface ISubjectRepository
    {
        Task<Subject> AddSubject(Subject subject);

        Task<Subject?> GetSubject(long id);

        Task<Subject?> GetSubject(string name);
    }
}
