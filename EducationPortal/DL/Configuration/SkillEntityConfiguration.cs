using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    class SkillEntityConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasCheckConstraint("skill_name_check", "len([Name]) >= 3");
            builder.HasIndex(i => i.Name).IsUnique();

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(20).IsRequired();
        }
    }
}
