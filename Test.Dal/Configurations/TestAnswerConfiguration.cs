using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class TestAnswerConfiguration : IEntityTypeConfiguration<TestAnswer>
    {
        public void Configure(EntityTypeBuilder<TestAnswer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.TestAttempt)
                .WithMany(x => x.Answers)
                .IsRequired();

            builder.HasMany(x => x.Answers)
                .WithMany();

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.IsCorrect)
                .IsRequired();
        }
    }
}
