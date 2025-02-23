using Authorize.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Authorize.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IDbContextTransaction? transaction;

        private RoleRepository roleRepository;
        private UserRepository userRepository;


        public UnitOfWork(
            AppDbContext context)
        {
            this.context = context;

            roleRepository = new RoleRepository(context);
            userRepository = new UserRepository(context);
        }

        public IRoleRepository RoleRepository => roleRepository;

        public IUserRepository UserRepository => userRepository;

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

        public void CommitTransaction()
        {
            if(transaction is null)
            {
                throw new InvalidOperationException("Commit Transaction when its null");
            }

            transaction.Commit();
        }

        public async Task CommitTransactionAsync()
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("Commit Transaction when its null");
            }

            await transaction.CommitAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void RollBackTransaction()
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("Commit Transaction when its null");
            }

            transaction.Rollback();
        }

        public async Task RollBackTransactionASync()
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("Commit Transaction when its null");
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
