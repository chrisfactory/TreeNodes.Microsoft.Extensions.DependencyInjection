using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("started");
            var rootServices = new ServiceCollection();
            rootServices.AddLogging(b => b.AddConsole());
            rootServices.AddSingleton<IService, CommonService1>();
            rootServices.AddSingleton<IService, CommonService2>();
            rootServices.AddSingleton<IService>(p => new CommonService3());
            //Service Branch Provider
            var branchProvider = rootServices.CreateBranch();

            rootServices.AddSingleton(p =>
            {
                //Create new ServiceCollection from Branch
                var module1Builder = branchProvider.CreateNewServicesFromBranch();

                var sub = module1Builder.CreateBranch();
                module1Builder.AddSingleton<IService>(p =>
               {
                   var ss = sub.CreateNewServicesFromBranch();
                   ss.AddSingleton<IService, Service1>();
                   var p1 = ss.BuildServiceProvider();
                   var tt = p1.GetRequiredService<IEnumerable<IService>>();
                   return p1.GetRequiredService<IService>();
               });
                 
                module1Builder.AddSingleton<IModule, Module1>();
                module1Builder.AddSingleton<IService, Service1>();
                return module1Builder.BuildServiceProvider().GetRequiredService<IModule>();
            });




            rootServices.AddSingleton(p =>
            {
                //Create new ServiceCollection and merge service descriptor from Branch
                //Same behavior as: => var module2Builder = branchProvider.CreateNewServicesFromBranch();
                var module2Builder = new ServiceCollection();
                branchProvider.MergeTo(module2Builder);


                module2Builder.AddSingleton<IModule, Module2>();
                module2Builder.AddSingleton<IService, Service2>();
                var m = module2Builder.BuildServiceProvider().GetRequiredService<IModule>();
                return m;
            });




            rootServices.AddSingleton<IGlobalService, GlobalService>();
            var provider = rootServices.BuildServiceProvider();
            var globalService = provider.GetRequiredService<IGlobalService>();
            var lastM = provider.GetRequiredService<IModule>();
            Console.WriteLine("finished");
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


    public class CommonService1 : IService
    {
        public CommonService1()
        {
        }
    }
    public class CommonService2 : IService { }
    public class CommonService3 : IService { }
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
