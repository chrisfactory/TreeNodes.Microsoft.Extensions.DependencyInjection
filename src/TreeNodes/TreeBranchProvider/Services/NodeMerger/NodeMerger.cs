﻿using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class NodeMerger : INodeMerger
    {
        private readonly IServiceValueResolver _resolver;
        public NodeMerger(IServiceValueResolver valueResolver)
        {
            _resolver = valueResolver;
        }

        public TServiceCollection MergeNodeTo<TServiceCollection>(TServiceCollection to)
            where TServiceCollection : IServiceCollection
        {
            foreach (var resolver in _resolver)
            {
                var descriptor = resolver.Descriptor;
                if (resolver.IsService)
                {
                    switch (descriptor.Lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            to.AddSingleton(descriptor.ServiceType, resolver.Get);
                            break;
                        case ServiceLifetime.Scoped:
                            to.AddScoped(descriptor.ServiceType, resolver.Get);
                            break;
                        case ServiceLifetime.Transient:
                            to.AddTransient(descriptor.ServiceType, resolver.Get);
                            break;
                        default:
                            break;
                    }
                }
                else
                    to.Add(descriptor);
            }
            return to;
        }

    }
}