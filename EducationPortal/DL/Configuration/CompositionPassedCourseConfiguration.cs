using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    class CompositionPassedCourseConfiguration : IEntityTypeConfiguration<CompositionPassedCourse>
    {
        public void Configure(EntityTypeBuilder<CompositionPassedCourse> builder)
        {
            builder.HasKey(i => new { i.CourseId, i.UserId});

            builder.HasOne(i => i.User)
                .WithMany(i => i.PassedCourses)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Course)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.CourseId);

        }
    }
}
