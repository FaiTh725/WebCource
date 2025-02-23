using Application.Shared.Exceptions;
using Authorize.Dal.Configurations;
using Authorize.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Authorize.Dal
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration): 
            base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration
                .GetConnectionString("SQLServerConnection") ??
                throw new AppConfigurationException("Sql Server Connection String");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
