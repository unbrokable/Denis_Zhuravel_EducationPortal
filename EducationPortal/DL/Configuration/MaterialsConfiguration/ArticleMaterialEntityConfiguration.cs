using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration.MaterialsConfiguration
{
    class ArticleMaterialEntityConfiguration : IEntityTypeConfiguration<ArticleMaterial>
    {
        public void Configure(EntityTypeBuilder<ArticleMaterial> builder)
        {
            builder.ToTable(nameof(ArticleMaterial));
            //builder.Property(i => i.)
        }
    }
}
