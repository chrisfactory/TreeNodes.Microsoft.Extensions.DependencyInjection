using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var rootServices = new ServiceCollection(); 
            rootServices.AddLogging(b => b.AddConsole()); 
            rootServices.AddSingleton<IService, CommonService>();

            //Service Branch Provider
            var branchProvider = rootServices.CreateBranchProvider();
             
            rootServices.AddSingleton(p =>
            {
                //Create new ServiceCollection from Branch
                var module1Builder = branchProvider.CreateNewServicesFromBranche();

                module1Builder.AddSingleton<IModule, Module1>();
                module1Builder.AddSingleton<IService, Service1>();
                return module1Builder.BuildServiceProvider().GetRequiredService<IModule>();
            });

            rootServices.AddSingleton(p =>
            {
                //Create new ServiceCollection and merge service descriptor from Branch
                //Same behavior as: => var module2Builder = branchProvider.CreateNewServicesFromBranche();
                var module2Builder = new ServiceCollection();
                branchProvider.MergeTo(module2Builder);
               

                module2Builder.AddSingleton<IModule, Module2>();
                module2Builder.AddSingleton<IService, Service2>();
                return module2Builder.BuildServiceProvider().GetRequiredService<IModule>();
            });




            rootServices.AddSingleton<IGlobalService, GlobalService>();

            var globalService = rootServices.BuildServiceProvider().GetRequiredService<IGlobalService>();


            Console.ReadLine();
        }
    }
    public interface IGlobalService
    {
        IReadOnlyList<IModule> Modules { get; }
    }
    public interface IModule
    {
        IReadOnlyList<IService> Services { get; }
    }
    public interface IService { }


    public class GlobalService : IGlobalService
    {
        public GlobalService(IEnumerable<IModule> modules)
        {
            Modules = modules.ToList();
        }
        public IReadOnlyList<IModule> Modules { get; }
    }


    public class Module1 : IModule
    {
        public Module1(IEnumerable<IService> services)
        {
            Services = services.ToList();
        }

        public IReadOnlyList<IService> Services { get; }
    }
    public class Module2 : IModule
    {
        public Module2(IEnumerable<IService> services)
        {
            Services = services.ToList();
        }

        public IReadOnlyList<IService> Services { get; }
    }


    public class CommonService : IService { }
    public class Service1 : IService
    {
        public Service1(ILogger<Service1> fromRootServices)
        {
            fromRootServices.LogInformation($"{nameof(Service1)}:  Logger from rootServices definition..");
        }

    }
    public class Service2 : IService
    {
        public Service2(ILogger<Service2> fromRootServices)
        {
            fromRootServices.LogInformation($"{nameof(Service2)}:  Logger from rootServices definition..");
        }
    }

}
