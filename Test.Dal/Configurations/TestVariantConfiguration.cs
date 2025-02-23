using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Dal.Configurations
{
    public class TestVariantConfiguration : IEntityTypeConfiguration<TestVariant>
    {
        public void Configure(EntityTypeBuilder<TestVariant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Test)
                .WithMany(x => x.Variants);

            builder.Property(x => x.Text)
                .IsRequired();
        }
    }
}
