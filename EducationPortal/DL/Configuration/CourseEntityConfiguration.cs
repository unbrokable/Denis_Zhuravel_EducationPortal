using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(30);
            builder.Property(i => i.Description).HasMaxLength(50);

            builder.HasMany(i => i.Skills)
                .WithMany(i => i.Courses);
            builder.HasMany(i => i.Materials)
                .WithMany(i => i.Courses);
                
        }
    }
}
