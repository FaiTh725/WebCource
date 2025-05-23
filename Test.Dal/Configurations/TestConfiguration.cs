﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEntity = Test.Domain.Entities.Test;

namespace Test.Dal.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasIndex(x => x.Id);

            builder.HasOne(x => x.Owner)
                .WithMany(x => x.CreatedTests);

            builder.HasOne(x => x.Subject)
                .WithMany(x => x.Tests);

            builder.HasMany(x => x.Questions)
                .WithOne(x => x.Test)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TestAccesses)
                .WithOne(x => x.Test)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
