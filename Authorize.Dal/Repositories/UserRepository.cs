using Authorize.Domain.Entities;
using Authorize.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Authorize.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(
            AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User> AddUser(User user)
        {
            var userEntity = await context.Users.AddAsync(user);

            return userEntity.Entity;
        }

        public async Task<Result<User>> GetUser(string email)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
            {
                return Result.Failure<User>("User doesnt found");
            }

            return user;
        }

        public async Task<Result<User>> GetUser(long id)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(user is null)
            {
                return Result.Failure<User>("User not found");
            }

            return user;
        }
    }
}
