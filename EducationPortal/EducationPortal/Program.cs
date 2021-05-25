using System;
using Ninject;
using Application.Interfaces;
using EducationPortal.Infrastructure;
using EducationPortal.Interfaces;

namespace EducationPortal
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceModuleUl ulModule = new ServiceModuleUl(AppDomain.CurrentDomain.BaseDirectory);
            var kernel = new StandardKernel(ulModule);
            var UserService = (IServiceUser)kernel.Get(typeof(IServiceUser));
            IAuthorizationManager authorization = kernel.Get<IAuthorizationManager>();
            authorization.Enter(); 
        }
        
    }
}
