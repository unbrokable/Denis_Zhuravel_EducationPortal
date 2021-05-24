using Ninject.Modules;
using EducationPortal.Interfaces;
using EducationPortal.Controllers;
using Application.Services;
using Application.Interfaces;
using Infrastructure.Repositories;
using Infrastructure;
using Application;

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
            Bind<IHasher>().To<Hasher>();
            Bind<IServiceUser>().To<UserService>();
            Bind<IEntitiesRepository>().To<EntitiesRepository>();
            Bind<IHandler>().To<BinaryHandler>().WithConstructorArgument(location);
            Bind<IAutoMapperBLConfiguration>().To<AutoMapperBLConfiguration>();
        }
    }
}
