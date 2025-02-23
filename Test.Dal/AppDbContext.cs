using Application.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.Dal.Configurations;
using Test.Domain.Entities;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Dal
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration) :
            base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TestEntity> Tests { get; set; }

        public DbSet<TestVariant> Questions { get; set; }

        public DbSet<TestAttempt> Attempts { get; set; }

        public DbSet<TestAnswer> AttemptsAnswers { get; set; }

        public DbSet<StudentGroup> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new TestVariantConfiguration());
            modelBuilder.ApplyConfiguration(new TestAttemptConfiguration());
            modelBuilder.ApplyConfiguration(new TestAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new StudentGroupConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("SQLServerConnection") ??
                throw new AppConfigurationException("SQLServer connection string"));
        }
    }
}
