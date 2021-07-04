using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    class CompositionPassedMaterialConfiguration : IEntityTypeConfiguration<CompositionPassedMaterial>
    {
        public void Configure(EntityTypeBuilder<CompositionPassedMaterial> builder)
        {
            builder.HasKey(i => new { i.MaterialId,i.UserId});

            builder.HasOne(i => i.User)
                .WithMany(i => i.PassedMaterials)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Material)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
