using Authorize.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Authorize.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);

        Task<Result<User>> GetUser(string email);

        Task<Result<User>> GetUser(long id);
    }
}
