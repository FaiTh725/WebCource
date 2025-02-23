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
                .WithMany(x => x.Answers);

            builder.Property(x => x.IsCorrect)
                .HasDefaultValue(false);
        }
    }
}
