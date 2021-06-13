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
            builder.HasIndex(i => i.Name).IsUnique();

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(30).IsRequired();
        }
    }
}
