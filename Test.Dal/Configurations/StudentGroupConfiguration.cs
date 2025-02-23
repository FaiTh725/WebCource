using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Students)
                .WithOne(x => x.Group);

            builder.HasIndex(x => x.GroupName)
                .IsUnique();
        }
    }
}
