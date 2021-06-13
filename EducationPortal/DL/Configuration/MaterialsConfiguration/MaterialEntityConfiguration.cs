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
            builder
                .HasOne(i => i.User)
                .WithMany(i => i.Materials)
                .HasForeignKey(i => i.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);
            //builder.Property(i => i.CreatorId).;
            builder.HasIndex(i => i.Name).IsUnique();
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(30).IsRequired();
            builder.Property(i => i.Location).HasMaxLength(40).IsRequired();
            
        }
    }
}
