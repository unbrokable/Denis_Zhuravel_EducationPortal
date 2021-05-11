using Ninject.Modules;
using System.Collections.Generic;
using DL.Interfaces;
using DL.Repositories;

namespace DL.Infrastructure
{
    public class ServiceModuleDL : NinjectModule
    {
        string location;
        public ServiceModuleDL(string location)
        {
            this.location = location;
        }
        public override void Load()
        {
            Bind(typeof(IRepository<User>)).To<UserRepository>();
            Bind(typeof(IContext<IEnumerable<User>>)).To(typeof(Context<IEnumerable<User>>)).WithConstructorArgument(location);
        }
    }
}
