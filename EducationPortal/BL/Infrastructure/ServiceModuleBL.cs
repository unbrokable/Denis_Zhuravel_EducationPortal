using BL.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Infrastructure;
using BL.Services;
using BL.DTO;

namespace BL.Infrastructure
{
    public class ServiceModuleBL : NinjectModule
    {
        
        public override void Load()
        {
            Bind<IHasher>().To<Hasher>();
            Bind(typeof(IService<UserDTO>)).To(typeof(UserService));
        }
    }
}
