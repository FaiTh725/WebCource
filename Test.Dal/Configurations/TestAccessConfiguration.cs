using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class TestAccessConfiguration :
        IEntityTypeConfiguration<TestAccess>
    {
        public void Configure(EntityTypeBuilder<TestAccess> builder)
        {
            builder.HasKey(x => new {x.TestId, x.StudentId});

            builder.HasOne(x => x.Test)
                .WithMany(x => x.TestAccesses)
                .HasForeignKey(x => x.TestId)
                .IsRequired();

            builder.HasOne(x => x.Student)
                .WithMany(x => x.TestAccesses)
                .HasForeignKey(x => x.StudentId)
                .IsRequired();

            builder.Property(x => x.TestDuration)
                .IsRequired();

            builder.ToTable(x =>
                x.HasCheckConstraint(
                    "CK_TestAccesses_TestDuration",
                    "TestDuration > 0"));

            //builder.ToTable(x =>
            //x.HasCheckConstraint(
            //    "CK_Attempts_Percent",
            //    "[Percent] >= 0 AND [Percent] <= 100"));
        }
    }
}
