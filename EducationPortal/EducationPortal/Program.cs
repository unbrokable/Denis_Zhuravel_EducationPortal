using System;
using Ninject;
using BL.Infrastructure;
using BL.Interfaces;
using EducationPortal.Infrastructure;
using EducationPortal.Interfaces;

namespace EducationPortal
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceModuleBL blModule = new ServiceModuleBL(AppDomain.CurrentDomain.BaseDirectory);
            ServiceModuleUl ulModule = new ServiceModuleUl();
            var kernel = new StandardKernel(blModule, ulModule);
            var UserService = (IServiceUser)kernel.Get(typeof(IServiceUser));
            IAuthorizationManager authorization = kernel.Get<IAuthorizationManager>();
            authorization.Enter(); 
        }
        
    }
}
