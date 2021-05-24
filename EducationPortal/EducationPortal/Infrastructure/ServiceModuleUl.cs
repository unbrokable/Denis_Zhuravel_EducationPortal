using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using EducationPortal.Interfaces;
using EducationPortal.Controllers;

namespace EducationPortal.Infrastructure
{
    class ServiceModuleUl : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthorizationManager>().To<AuthorizationManager>();
        }
    }
}
