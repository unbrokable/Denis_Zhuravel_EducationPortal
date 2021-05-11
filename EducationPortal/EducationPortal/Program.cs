using System;
using BL.Services;
using EducationPortal.Controllers;
using Ninject.Modules;
using DL.Infrastructure;
using Ninject;
using DL.Repositories;
using DL;
using BL.Infrastructure;
using System.Collections.Generic;
using BL.Interfaces;
using DL.Interfaces;
using BL.DTO;

namespace EducationPortal
{
    class Program
    {
        static void Main(string[] args)
        {
           
            NinjectModule dlModule = new ServiceModuleDL("../../../../userstest.json");
            ServiceModuleBL blModule = new ServiceModuleBL();  
            var kernel = new StandardKernel(dlModule, blModule);
            var UserService = (UserService)kernel.Get(typeof(IService<UserDTO>));
            AuthorizationController authorization = new AuthorizationController(UserService);
            authorization.Index();
           
        }
        
    }
}
