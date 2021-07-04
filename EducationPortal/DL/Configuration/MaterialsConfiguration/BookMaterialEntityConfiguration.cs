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
            builder.HasCheckConstraint("bookmaterial_author_check", "LEN([Author])>= 5");
            builder.HasCheckConstraint("bookmaterial_format_check", "LEN([Format]) >= 3");


            builder.ToTable(nameof(BookMaterial));
            builder.Property(i => i.Author).HasMaxLength(20);
            builder.Property(i => i.Format).HasMaxLength(10);
            builder.Property(i => i.DateOfPublished).IsRequired();
        
        }
    }
}
