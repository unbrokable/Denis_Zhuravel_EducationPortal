using System;
using Ninject;
using Application.Interfaces;
using EducationPortal.Infrastructure;
using EducationPortal.Interfaces;
using Ninject.Modules;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System.Threading.Tasks;

namespace EducationPortal
{
    class Program
    {
        public static async Task Main()
        {
            var container = Startup.ConfigureService();
            var mainManager = (IManager)container.GetService(typeof(IManager));
            await mainManager.StartAsync();
            Console.ReadKey();
        }
        
    }
}
