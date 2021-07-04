using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    class CompositionSkillUserConfiguration : IEntityTypeConfiguration<CompositionSkillUser>
    {
        public void Configure(EntityTypeBuilder<CompositionSkillUser> builder)
        {
            builder.HasKey(i => new { i.SkillId , i.UserId});
            builder.HasOne(i => i.User)
                .WithMany(i => i.Skills)
                .HasForeignKey(i => i.UserId);
            builder.HasOne(i => i.Skill)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.SkillId);
                
        }
    }
}
