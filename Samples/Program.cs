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



            var node = rootServices.CreateNode("sample.root.node");

            rootServices.AddSingleton(p =>
            { 
                var module1Builder = node.CreateBranch();
                module1Builder.AddSingleton<IModule, Module1>();
                module1Builder.AddSingleton<IService, Service1>();
                return module1Builder.BuildServiceProvider().GetRequiredService<IModule>();
            });


            rootServices.AddSingleton(p =>
            { 
                var module1Builder = p.GetNode("sample.root.node").CreateBranch(); 
                module1Builder.AddSingleton<IModule, Module2>();
                module1Builder.AddSingleton<IService, Service2>();
                return module1Builder.BuildServiceProvider().GetRequiredService<IModule>();
            });
             
            rootServices.AddSingleton(p =>
            { 
                var module2Builder = new ServiceCollection();
                node.InsertBranchStack(module2Builder);  
                module2Builder.AddSingleton<IModule, Module3>();
                module2Builder.AddSingleton<IService, Service3>();
                var m = module2Builder.BuildServiceProvider().GetRequiredService<IModule>();
                return m;
            });




            rootServices.AddSingleton<IGlobalService, GlobalService>();
            var provider = rootServices.BuildServiceProvider();
            var globalService = provider.GetRequiredService<IGlobalService>(); 
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
    public class Module3 : IModule
    {
        public Module3(IEnumerable<IService> services)
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
    public class Service3 : IService
    {
        public Service3(ILogger<Service3> fromRootServices)
        {
            fromRootServices.LogInformation($"{nameof(Service3)}:  Logger from rootServices definition..");
        }
    }
}
