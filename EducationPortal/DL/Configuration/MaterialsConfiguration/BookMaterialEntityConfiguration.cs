using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration.MaterialsConfiguration
{
    class BookMaterialEntityConfiguration : IEntityTypeConfiguration<BookMaterial>
    {
        public void Configure(EntityTypeBuilder<BookMaterial> builder)
        {
            builder.ToTable(nameof(BookMaterial));
            builder.Property(i => i.Author).HasMaxLength(50);
            builder.Property(i => i.Format).HasMaxLength(20);
           
        }
    }
}
