using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface IServiceCollectionSnapshot
    {
        IServiceProvider Provider { get; }
        IReadOnlyCollection<ServiceDescriptor> Services { get; }
    }
}
