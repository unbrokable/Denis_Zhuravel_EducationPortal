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
            builder.HasCheckConstraint("videomaterial_length_check", "[Length] > 0 ");
            builder.HasCheckConstraint("videomaterial_height_check", "[Height] > 0");
            builder.HasCheckConstraint("videomaterial_width_check", "[Width] > 0");

            builder.ToTable(nameof(VideoMaterial));
            builder.Property(i => i.Length).HasColumnType("bigint");
        }
    }
}
