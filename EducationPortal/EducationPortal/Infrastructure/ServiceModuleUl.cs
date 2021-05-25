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

namespace EducationPortal.Infrastructure
{
    class ServiceModuleUl : NinjectModule
    {
        readonly string location;
        public ServiceModuleUl(string location)
        {
            this.location = location;
        }
        public override void Load()
        {
            Bind<IAuthorizationManager>().To<AuthorizationManager>();
            Bind<ICourseManager>().To<CourseManager>();
            Bind<IManager>().To<MainManager>();
            Bind<IMaterialManager>().To<MaterialManager>();
            Bind<IAutoMapperUlConfiguration>().To<AutoMapperUlConfiguration>();
            Bind<IHasher>().To<Hasher>();
            Bind<IServiceUser>().To<UserService>();
            Bind(typeof(IServiceEntities<MaterialDTO>)).To(typeof(MaterialService));
            Bind(typeof(IServiceEntities<CourseDTO>)).To(typeof(CourseService));
            Bind<IEntitiesRepository>().To<EntitiesRepository>();
            Bind<IHandler>().To<BinaryHandler>().WithConstructorArgument(location);
            Bind<IAutoMapperBLConfiguration>().To<AutoMapperBLConfiguration>();
        }
    }
}
