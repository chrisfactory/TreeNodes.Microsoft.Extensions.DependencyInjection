using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface IServiceSnapshot
    {
        IServiceProvider Provider { get; }
        IReadOnlyCollection<ServiceDescriptor> Services { get; }
    }
}
