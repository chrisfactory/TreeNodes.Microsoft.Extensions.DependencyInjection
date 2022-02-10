using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceValueResolver : IServiceValueResolver
    {
        private readonly IReadOnlyCollection<IValueResolver> _resolver;
        public ServiceValueResolver(IServiceSnapshot snapshot)
        {
            _resolver = InitializeResolver(snapshot);
        }

        private static IReadOnlyCollection<IValueResolver> InitializeResolver(IServiceSnapshot snapshot)
        {
            var checkService = snapshot.Provider.GetRequiredService<IServiceProviderIsService>();
            var resolverDescriptors = new List<ResolverDescritor>();
            var _dic = new Dictionary<Type, ResolverProxy>();
            foreach (var servicesDescriptor in snapshot.Services)
            {
                if (!_dic.ContainsKey(servicesDescriptor.ServiceType))
                    _dic.Add(servicesDescriptor.ServiceType, new ResolverProxy(servicesDescriptor.ServiceType));
                resolverDescriptors.Add(new ResolverDescritor(servicesDescriptor, _dic[servicesDescriptor.ServiceType], checkService.IsService(servicesDescriptor.ServiceType)));
            }
            return resolverDescriptors.Select(descriptor => descriptor.CreateResolver(snapshot.Provider)).ToList();
        }

        public IEnumerator<IValueResolver> GetEnumerator() => _resolver.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_resolver).GetEnumerator();
    }
}

