using Microsoft.Extensions.DependencyInjection;
namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class SnapshotBranchMergerService : ISnapshotBranchMergerService
    {
        private readonly IServiceValueResolver _resolver;
        public SnapshotBranchMergerService(IServiceValueResolver valueResolver)
        {
            _resolver = valueResolver;
        }

        public TServiceCollection MergeBranchTo<TServiceCollection>(TServiceCollection to)
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
