using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using EducationPortal.Interfaces;
using EducationPortal.Controllers;
using EducationPortal.Managers;
using Application.Interfaces;
using Application;
using Application.Services;
using Application.DTO.MaterialDTOs;
using Application.DTO;
using Infrastructure.Repositories;
using Application.Interfaces.IServices;
using EducationPortal.Managers.Commands;
using EducationPortal.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace EducationPortal
{
    class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()

                .AddDbContext<ApplicationContext>(
                    opt =>
                    {
                        var path = String.Concat(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"\appsettings.json");
                        string connection = (new ConfigurationBuilder()
                            .AddJsonFile(path, true, true)
                            .Build())
                            .GetSection("ConnectionStrings")
                            .GetSection("SecurityConnect").Value;
                        opt.UseSqlServer(connection);
                    })
                .AddScoped<IAuthorizationManager,AuthorizationManager>()
                .AddScoped<ICourseManager,CourseManager>()
                .AddScoped<IManager,MainManager>()
                .AddScoped<IMaterialManager,MaterialManager>()
                .AddScoped<IPassCourseManager,PassCourseManager>()
                .AddScoped<ISkillManager,SkillManager>()

                .AddScoped<IHasher,Hasher>()
                .AddTransient<IServiceUser,UserService>() 
                .AddTransient<IServiceMaterial, MaterialService>()
                .AddTransient<ICoursePassingService,CoursePassingService>()
                .AddTransient<IEntitiesRepository,EntitiesRepository>()
                .AddTransient<IServiceSkill,SkillService>()
                .AddTransient< IServiceCourse,CourseService>()
               

                //mappers
                .AddTransient<IAutoMapperUlConfiguration ,AutoMapperUlConfiguration>()
                .AddTransient<IAutoMapperBLConfiguration,AutoMapperBLConfiguration>()

                //commands
                .AddScoped <ICommand<MaterialViewModel>,MaterialCreateCommands>()
                .AddScoped <IEnumerable<ICommandMaterial>>(opt => {
                    return new ICommandMaterial[] {
                        new VideoMaterialCreateCommand(),
                        new BookMaterialCreateCommand(),
                        new ArticleMaterialCreateCommand()
                    };
                })
                .AddScoped<ILogger>(i => {
                    ILoggerFactory loggerFactory = LoggerFactory.Create(i => {
                        
                        var path = String.Concat(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"\appsettings.json");
                        var conf = (new ConfigurationBuilder()
                            .AddJsonFile(path, true, true)
                            .Build())
                            .GetSection("Logging");
                        i.AddConfiguration(conf);
                        i.AddDebug();
                        i.AddFile("../../../../LoggingInfo",LogLevel.Warning);
                    });
                    return loggerFactory.CreateLogger<Program>();
                })
                .BuildServiceProvider();

            return provider;
        }

        public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args = null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                var path = String.Concat(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"\appsettings.json");
                //Dont work during migration and  i cant fix it
                //string connection  = (new ConfigurationBuilder()
                //    .AddJsonFile(path, true, true)
                //    .Build())
                //    .GetSection("ConnectionStrings")
                //    .GetSection("SecurityConnect").Value;
                //Work but don't kill me (Please)
                string connection = "data source = localhost; initial catalog = EducationPortalZhuravel; Trusted_Connection = True; multipleactiveresultsets = True;";
                optionsBuilder.UseSqlServer(connection);
                return new ApplicationContext(optionsBuilder.Options);
            }
        }
    }
}
