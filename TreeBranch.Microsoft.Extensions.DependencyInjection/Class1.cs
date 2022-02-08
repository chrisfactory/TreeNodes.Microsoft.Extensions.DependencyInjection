using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{

    public static partial class IServiceCollectionExtensions
    {
        internal static IServiceCollection Copy(this IServiceCollection source)
        {
            var array = new ServiceDescriptor[source.Count];
            source.CopyTo(array, 0);
            var services = (IServiceCollection)new ServiceCollection();
            foreach (var service in array)
                services.Add(service);
            return services;
        }


        internal static void MergeBrancheTo(this IServiceCollection snapshot, IServiceProvider providerSource, IServiceCollection to)
        {
            var checkServices = providerSource.GetRequiredService<IServiceProviderIsService>();
            var orderedServices = new Dictionary<Type, ServiceResolver>();
            foreach (var item in snapshot)
            {
                if (!orderedServices.ContainsKey(item.ServiceType))
                    orderedServices.Add(item.ServiceType, new ServiceResolver(providerSource));
                orderedServices[item.ServiceType].Add(item);

                var isService = checkServices.IsService(item.ServiceType);
                if (isService)
                {
                    switch (item.Lifetime)
                    {

                        case ServiceLifetime.Singleton:
                            to.AddSingleton(item.ServiceType, p => orderedServices[item.ServiceType].Get(item));
                            break;
                        case ServiceLifetime.Scoped:
                            to.AddScoped(item.ServiceType, p => orderedServices[item.ServiceType].Get(item));
                            break;
                        case ServiceLifetime.Transient:
                            to.AddTransient(item.ServiceType, p => orderedServices[item.ServiceType].Get(item));
                            break;
                        default:
                            break;
                    }
                }
                else
                    to.Add(item);
            }
        }
        private class ServiceResolver
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
            private DifferedIterrator _diif = null;
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
        public class DifferedIterrator
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


        public static IServiceCollectioBrancheProvider CreateBrancheProvider(this IServiceCollection source)
        {
            lock (source)
            {
                var snapshot = source.Copy();
                var provider = source.BuildServiceProvider();
                source.Clear();
                var brancheProvider = new ServiceCollectioBrancheProvider(snapshot, provider);
                brancheProvider.MergeTo(source);
                return brancheProvider;
            }
        }


        private class ServiceCollectioBrancheProvider : IServiceCollectioBrancheProvider
        {
            private readonly IServiceCollection snapshot;
            private readonly IServiceProvider provider;

            public ServiceCollectioBrancheProvider(IServiceCollection snapshot, IServiceProvider provider)
            {
                this.snapshot = snapshot;
                this.provider = provider;
            }

            public ServiceCollection CreateBranche()
            {
                var branche = new ServiceCollection();
                MergeTo(branche);
                return branche;
            }

            public void MergeTo(params IServiceCollection[] sources)
            {
                foreach (var source in sources)
                    snapshot.MergeBrancheTo(provider, source);
            }
        }
    }
    public interface IServiceCollectioBrancheProvider
    {
        ServiceCollection CreateBranche();
        void MergeTo(params IServiceCollection[] sources);
    }


}