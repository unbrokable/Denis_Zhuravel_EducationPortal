using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasCheckConstraint("user_email_check", "len([Email]) >=  10");
            builder.HasCheckConstraint("user_name_check", "len([Name]) >= 5");
            builder.HasCheckConstraint("user_password_check", "len([Password]) >= 5");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(i => i.Password).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Email).IsRequired().HasMaxLength(30);
           

        }
    }
}
