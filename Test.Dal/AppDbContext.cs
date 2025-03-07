using Application.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.Dal.Configurations;
using Test.Domain.Entities;
using Test.Domain.Primitives;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Dal
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;
        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration,
            IMediator mediator) :
            base(options)
        {
            this.configuration = configuration;
            this.mediator = mediator;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.DomainEvents;

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
