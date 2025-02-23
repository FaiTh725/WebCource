using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class TestAttemptConfiguration : IEntityTypeConfiguration<TestAttempt>
    {
        public void Configure(EntityTypeBuilder<TestAttempt> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AnswerStudent)
                .WithMany();

            builder.HasOne(x => x.Test)
                .WithMany();

            builder.HasMany(x => x.Answers)
                .WithOne(x => x.TestAttempt)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Percent)
                .HasDefaultValue(0);

            builder.ToTable(x => 
            x.HasCheckConstraint(
                "CK_Attempts_Percent", 
                "[Percent] >= 0 AND [Percent] <= 100"));  
        }
    }
}
