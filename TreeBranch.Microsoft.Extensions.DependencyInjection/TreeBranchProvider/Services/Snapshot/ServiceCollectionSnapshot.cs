using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceCollectionSnapshot : IServiceCollectionSnapshot
    {
        public ServiceCollectionSnapshot(IServiceSource serviceSource)
        { 
            Services = serviceSource.Source.ToArray();
            Provider = serviceSource.Source.BuildServiceProvider(); 
        }
        public IServiceProvider Provider { get; }
        public IReadOnlyCollection<ServiceDescriptor> Services { get; }
    }
}
