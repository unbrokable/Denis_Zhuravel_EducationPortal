using BL.Interfaces;
using Ninject.Modules;
using BL.Services;
using DL.Interfaces;
using DL;
using DL.Repositories;

namespace BL.Infrastructure
{
    public class ServiceModuleBL : NinjectModule
    {
        readonly string location;
        public ServiceModuleBL(string location)
        {
            this.location = location;
        }

        public override void Load()
        {
            Bind<IHasher>().To<Hasher>();
            Bind<IServiceUser>().To<UserService>();
            Bind<IEntitiesRepository>().To<EntitiesRepository>();
            Bind<IHandler>().To<BinaryHandler>().WithConstructorArgument(location);
            Bind<IAutoMapperBLConfiguration>().To<AutoMapperBLConfiguration>();
        }
    }
}
