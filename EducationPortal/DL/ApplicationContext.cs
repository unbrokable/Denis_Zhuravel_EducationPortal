using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Material> Materials { get; set; }
        public DbSet<VideoMaterial> VideoMaterials { get; set; }
        public DbSet<BookMaterial> BookMaterials { get; set; }
        public DbSet<ArticleMaterial> ArticleMaterials { get; set; }

        public DbSet<CompositionPassedCourse> PassedCourses { get; set;}
        public DbSet<CompositionPassedMaterial> PassedMaterials { get; set;}
        public DbSet<CompositionSkillUser> UserSkills { get; set; }


        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=EducationPortalZhuravel;Trusted_Connection=True;multipleactiveresultsets=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
