using Authorize.Domain.Entities;
using Authorize.Domain.Repositories;
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

        public async Task<User?> GetUser(string email)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User?> GetUser(long id)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task UpdateUser(long userId, User userNewValue)
        {
            await context.Users
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.Role, userNewValue.Role)
                .SetProperty(p => p.PasswordHash, userNewValue.PasswordHash));
        }
    }
}
