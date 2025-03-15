using Microsoft.EntityFrameworkCore.Storage;
using Test.Domain.Repositories;

namespace Test.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IDbContextTransaction? transaction;

        private readonly StudentRepository studentRepository;
        private readonly TeacherRepository teacherRepository;
        private readonly GroupRepository groupRepository;
        private readonly SubjectRepository subjectRepository;
        private readonly TestRepository testRepository;
        private readonly QuestionRepository questionRepository;
        private readonly QuestionVariantRepository questionVariantRepository;
        private readonly TestAttemptRepository testAttemptRepository;

        public UnitOfWork(
            AppDbContext context,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            ITestRepository testRepository,
            IQuestionRepository questionRepository,
            IQuestionVariantRepository questionVariantRepository,
            ITestAttemptRepository testAttemptRepository
            )
        {
            this.context = context;

            this.studentRepository = (StudentRepository)studentRepository;
            this.teacherRepository = (TeacherRepository)teacherRepository;
            this.groupRepository = (GroupRepository)groupRepository;
            this.subjectRepository = (SubjectRepository)subjectRepository;
            this.testRepository = (TestRepository)testRepository;
            this.questionRepository = (QuestionRepository)questionRepository;
            this.questionVariantRepository = (QuestionVariantRepository)questionVariantRepository;
            this.testAttemptRepository = (TestAttemptRepository)testAttemptRepository;
        }

        public IStudentRepository StudentRepository => studentRepository;

        public ITeacherRepository TeacherRepository => teacherRepository;

        public IGroupRepository GroupRepository => groupRepository;

        public ISubjectRepository SubjectRepository => subjectRepository;

        public ITestRepository TestRepository => testRepository;

        public IQuestionRepository QuestionRepository => questionRepository;

        public IQuestionVariantRepository QuestionVariantRepository => questionVariantRepository;

        public ITestAttemptRepository TestAttemptRepository => testAttemptRepository;

        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await context.Database.BeginTransactionAsync();
        }

        public bool CanConnect()
        {
            return context.Database.CanConnect();
        }

        public async Task<bool> CanConnectAsync()
        {
            return await context.Database.CanConnectAsync();
        }

        public async Task CommiTransactionAsync()
        {
            if(transaction is null)
            {
                throw new InvalidOperationException("Transaction is null");
            }

            await transaction.CommitAsync();
        }

        public void CommitTransaction()
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("Transaction is null");
            }

            transaction.Commit();
        }

        public void RollBackTransaction()
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("Transaction is null");
            }

            transaction.Rollback();
        }

        public async Task RollBackTransactionAsync()
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("Transaction is null");
            }

            await transaction.RollbackAsync();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
