using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Tests)
                .WithOne(x => x.Subject)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
