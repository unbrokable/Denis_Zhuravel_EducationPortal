using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration.MaterialsConfiguration
{
    class VideoMaterialEntityConfiguration : IEntityTypeConfiguration<VideoMaterial>
    {
        public void Configure(EntityTypeBuilder<VideoMaterial> builder)
        {
            builder.ToTable(nameof(VideoMaterial));
            builder.Property(i => i.Length).HasColumnType("bigint");
        }
    }
}
