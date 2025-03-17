using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasMany(x => x.TestAccesses)
                .WithOne(x => x.Student)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Profile.MAX_NAME_LENGTH);

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Students);
        }
    }
}
