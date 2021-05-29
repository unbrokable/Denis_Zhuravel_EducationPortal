using Ninject.Modules;
using EducationPortal.Interfaces;
using EducationPortal.Controllers;
using Application.Services;
using Application.Interfaces;
using Infrastructure.Repositories;
using Infrastructure;
using Application;
using Application.DTO.MaterialDTOs;
using Application.DTO;
using EducationPortal.Models;
using EducationPortal.Managers.Commands;

namespace EducationPortal.Infrastructure
{
    class ServiceModuleUl : NinjectModule
    {
        private readonly string location;
        public ServiceModuleUl(string location)
        {
            this.location = location;
        }
        public override void Load()
        {
            //UI
            Bind<IAuthorizationManager>().To<AuthorizationManager>();
            Bind<ICourseManager>().To<CourseManager>();
            Bind<IManager>().To<MainManager>();
            Bind<IMaterialManager>().To<MaterialManager>();
          
            Bind<IHasher>().To<Hasher>();
            Bind<IServiceUser>().To<UserService>();
            Bind(typeof(IServiceEntities<MaterialDTO>)).To(typeof(MaterialService));
            Bind(typeof(IServiceEntities<CourseDTO>)).To(typeof(CourseService));
            Bind<IEntitiesRepository>().To<EntitiesRepository>();
            Bind<IHandler>().To<BinaryHandler>().WithConstructorArgument(location);

            //mappers
            Bind<IAutoMapperUlConfiguration>().To<AutoMapperUlConfiguration>();
            Bind<IAutoMapperBLConfiguration>().To<AutoMapperBLConfiguration>();

            //commands
            Bind(typeof(ICommand<MaterialViewModel>)).To(typeof(MaterialCreateCommands));
            Bind<ICommandMaterial>().To<VideoMaterialCreateCommand>();
            Bind<ICommandMaterial>().To<BookMaterialCreateCommand>();
            Bind<ICommandMaterial>().To<ArticleMaterialCreateCommand>();
        }
    }
}
