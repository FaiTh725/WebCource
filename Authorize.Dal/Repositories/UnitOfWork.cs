using Authorize.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authorize.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IDbContextTransaction? transaction;

        private RoleRepository roleRepository;
        private UserRepository userRepository;
        private readonly RefreshTokenRepository refreshTokenRepository;


        public UnitOfWork(
            AppDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.context = context;

            this.roleRepository = (RoleRepository)roleRepository;
            this.userRepository = (UserRepository)userRepository;
            this.refreshTokenRepository = (RefreshTokenRepository)refreshTokenRepository;
        }

        public IRoleRepository RoleRepository => roleRepository;

        public IUserRepository UserRepository => userRepository;

        public IRefreshTokenRepository RefreshTokenRepository => refreshTokenRepository;

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

        public async Task RollBackTransactionAsync()
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
