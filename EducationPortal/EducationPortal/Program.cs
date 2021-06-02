using System;
using Ninject;
using Application.Interfaces;
using EducationPortal.Infrastructure;
using EducationPortal.Interfaces;
using Ninject.Modules;

namespace EducationPortal
{
    class Program
    {
        static void Main(string[] args)
        {
            NinjectModule ulModule = new ServiceModuleUl(AppDomain.CurrentDomain.BaseDirectory);
            var kernel = new StandardKernel(ulModule);
            var mainManager = kernel.Get<IManager>();
            mainManager.Start();
        }
        
    }
}
