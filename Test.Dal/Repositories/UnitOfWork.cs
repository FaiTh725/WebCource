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

        public UnitOfWork(
            AppDbContext context
            )
        {
            this.context = context;

            studentRepository = new StudentRepository(context);
            teacherRepository = new TeacherRepository(context);
            groupRepository = new GroupRepository(context);
        }

        public IStudentRepository StudentRepository => studentRepository;

        public ITeacherRepository TeacherRepository => teacherRepository;

        public IGroupRepository GroupRepository => groupRepository;

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
