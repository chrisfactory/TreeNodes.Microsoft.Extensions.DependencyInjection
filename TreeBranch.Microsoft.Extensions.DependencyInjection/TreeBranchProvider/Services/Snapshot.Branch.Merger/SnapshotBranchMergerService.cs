using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class SnapshotBranchMergerService : ISnapshotBranchMergerService
    {
        private readonly IServiceCollectionSnapshot _snapshot;
        private readonly IServiceProviderIsService _checkServices;
        public SnapshotBranchMergerService(IServiceCollectionSnapshot snapshot)
        {
            _snapshot = snapshot;
            _checkServices = snapshot.Provider.GetRequiredService<IServiceProviderIsService>();
        }
        public void MergeBranchTo(IServiceCollection to)
        { 
            var orderedServices = new Dictionary<Type, ServiceResolver>();
            foreach (var originalService in _snapshot.Services)
            {
                if (!orderedServices.ContainsKey(originalService.ServiceType))
                    orderedServices.Add(originalService.ServiceType, new ServiceResolver(_snapshot.Provider));
                orderedServices[originalService.ServiceType].Add(originalService);
                if (_checkServices.IsService(originalService.ServiceType))
                {
                    switch (originalService.Lifetime)
                    {

                        case ServiceLifetime.Singleton:
                            to.AddSingleton(originalService.ServiceType, p => orderedServices[originalService.ServiceType].Get(originalService));
                            break;
                        case ServiceLifetime.Scoped:
                            to.AddScoped(originalService.ServiceType, p => orderedServices[originalService.ServiceType].Get(originalService));
                            break;
                        case ServiceLifetime.Transient:
                            to.AddTransient(originalService.ServiceType, p => orderedServices[originalService.ServiceType].Get(originalService));
                            break;
                        default:
                            break;
                    }
                }
                else
                    to.Add(originalService);
            }
        }
    }

    internal class ServiceResolver
    {
        private readonly IServiceProvider _resolver;
        private readonly List<ServiceDescriptor> _services = new List<ServiceDescriptor>();

        public ServiceResolver(IServiceProvider provider)
        {
            _resolver = provider;
        }
        public void Add(ServiceDescriptor descriptor)
        {
            _services.Add(descriptor);
        }
        private DifferedIterrator? _diif = null;
        public object Get(ServiceDescriptor item)
        {
            var last = _services.Count - 1;
            var requestIndex = _services.IndexOf(item);
            if (last != requestIndex && requestIndex == 0)
                _diif = new DifferedIterrator(item.ServiceType, _resolver);
            if (_diif != null)
            {
                var result = _diif.Get(requestIndex, out var finished);
                if (finished)
                    _diif = null;
                return result;
            }

            return _resolver.GetRequiredService(item.ServiceType);
        }
    }
    internal class DifferedIterrator
    {
        private readonly IReadOnlyList<object> values;
        public DifferedIterrator(Type tx, IServiceProvider provider)
        {
            var serviceType2 = typeof(IEnumerable<>).MakeGenericType(tx);
            values = ((IEnumerable<object>)provider.GetRequiredService(serviceType2)).ToList();
        }

        internal object Get(int requestIndex, out bool finished)
        {
            finished = (values.Count - 1) == requestIndex;
            var result = values[requestIndex];
            return result;
        }
    } 
}
