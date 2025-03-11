using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Test)
                .WithMany(x => x.Questions);

            builder.HasMany(x => x.Variants)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Question)
                .IsRequired();
        }
    }
}
