using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    class MaterialEntityConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasCheckConstraint("material_location_check", "[Location] <> '' ");
            builder.HasCheckConstraint("material_name_check", "len([Name]) >= 5");

            builder
                .HasOne(i => i.User)
                .WithMany(i => i.Materials)
                .HasForeignKey(i => i.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasIndex(i => i.Name).IsUnique();
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(20).IsRequired();
            builder.Property(i => i.Location).HasMaxLength(50).IsRequired();
            
        }
    }
}
