using System.Data;

namespace Authorize.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }

        public IRefreshTokenRepository RefreshTokenRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void BeginTransaction();

        Task BeginTransactionAsync();

        void CommitTransaction();

        Task CommitTransactionAsync();

        void RollBackTransaction();

        Task RollBackTransactionAsync();
        
        bool CanConnect();

        Task<bool> CanConnectAsync();

    }
}
