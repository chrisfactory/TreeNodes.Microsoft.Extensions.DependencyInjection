using Microsoft.Extensions.DependencyInjection;
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
            rootServices.AddSingleton<ISubService, CommonSubService>();



            var branchProvider = rootServices.CreateBranchProvider();


            
            rootServices.AddSingleton(p =>
            {
                var subModuleBuilder = branchProvider.CreateNewServicesFromBranche();

                subModuleBuilder.AddSingleton<IModuleService, ModuleService2>();
                subModuleBuilder.AddSingleton<ISubService, M2SubService>();
                return subModuleBuilder.BuildServiceProvider().GetRequiredService<IModuleService>();
            });


            rootServices.AddSingleton<IModuleService, ModuleService1>();
            rootServices.AddSingleton<ISubService, M1SubService>();


            var modules = rootServices.BuildServiceProvider().GetRequiredService<IEnumerable<IModuleService>>().ToList();
            Console.ReadLine();
        }
    }

    public interface IModuleService
    {
        IReadOnlyList<ISubService> SubServices { get; }
    }
    public interface ISubService { }

    public class ModuleService1 : IModuleService
    {
        public ModuleService1(IEnumerable<ISubService> subServices)
        {
            SubServices = subServices.ToList();
        }

        public IReadOnlyList<ISubService> SubServices { get; }
    }




    public class ModuleService2 : IModuleService
    {
        public ModuleService2(IEnumerable<ISubService> subServices)
        {
            SubServices = subServices.ToList();
        }

        public IReadOnlyList<ISubService> SubServices { get; }
    }


    public class CommonSubService : ISubService { }
    public class M1SubService : ISubService
    {

    }

    public class M2SubService : ISubService
    {

    }

}
