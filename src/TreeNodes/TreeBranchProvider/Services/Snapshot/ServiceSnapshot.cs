using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceSnapshot : IServiceSnapshot
    {
        public ServiceSnapshot(ISourceInjector serviceSource)
        {
            Services = serviceSource.Source.ToArray();
            Provider = serviceSource.Source.BuildServiceProvider();
        }
        public IServiceProvider Provider { get; }
        public IReadOnlyCollection<ServiceDescriptor> Services { get; }
    }
}
