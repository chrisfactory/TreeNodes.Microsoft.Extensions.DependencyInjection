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



            var loggingNode = new ServiceCollection()
                                    .AddLogging(b => b.AddConsole())
                                    .CreateNode("sample.common.logging");


            var commonServicesNode = new ServiceCollection()
                                    .AddSingleton<IService, CommonService1>()
                                    .AddTransient<IService, CommonService2>()
                                    .AddSingleton<IService>(p => new CommonService3())
                                    .CreateNode("sample.common.services");


            var auther = new ServiceCollection();
            //...
            var otherNode = auther.CreateNode("sample.other...");


            var commonNode = (loggingNode + commonServicesNode + otherNode).CreateNode("sample.common");

            var appServices = new ServiceCollection() + commonNode;//Similar to: commonNode.CreateBranch();



            appServices.AddSingleton(p =>
            {
                var builder = p.GetNode("sample.common").CreateBranch();
                builder.AddSingleton<IModule, Module1>();
                builder.AddSingleton<IService, Service1>();
                return builder.BuildServiceProvider().GetRequiredService<IModule>();
            });


            appServices.AddSingleton(p =>
            {
                var builder = p.GetNode("sample.common.logging").CreateBranch();
                builder.AddSingleton<IModule, Module2>();
                builder.AddSingleton<IService, Service2>();
                return builder.BuildServiceProvider().GetRequiredService<IModule>();
            });


            appServices.AddSingleton(p =>
            {
                var subModulServices = p.GetNode("sample.common.services").CreateBranch();
                //define custom logger
                subModulServices.AddLogging(b => b.AddSystemdConsole());

                subModulServices.AddSingleton<IModule, Module3>();
                subModulServices.AddSingleton<IService, Service3>();
                return subModulServices.BuildServiceProvider().GetRequiredService<IModule>();
            });
             
            appServices.AddSingleton(p =>
            {
                var subModulServices = commonNode.CreateBranch();
                subModulServices.AddSingleton<IModule, Module4>();
                subModulServices.AddSingleton<IService, Service4>();
                return subModulServices.BuildServiceProvider().GetRequiredService<IModule>();
            });

            appServices.AddSingleton<IGlobalService, GlobalService>();

            var provider = appServices.BuildServiceProvider();
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
    public class Module4 : IModule
    {
        public Module4(IEnumerable<IService> services)
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
            fromRootServices.LogInformation($"{nameof(Service3)}:  Custom Logger ...");
        }
    }

    public class Service4 : IService
    {
        public Service4(ILogger<Service2> fromRootServices)
        {
            fromRootServices.LogInformation($"{nameof(Service4)}:  Logger from rootServices definition..");
        }
    }
}
