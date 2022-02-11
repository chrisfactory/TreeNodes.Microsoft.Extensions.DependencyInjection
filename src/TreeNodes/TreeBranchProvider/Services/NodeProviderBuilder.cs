using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class NodeProviderBuilder : INodeProviderBuilder
    {
        public NodeProviderBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<IInternalNodeSnapshotPoint, NodeSnapshotPoint>();
            Services.AddSingleton<INodeSnapshotPoint>(p=>p.GetRequiredService<IInternalNodeSnapshotPoint>());
        }
        public IServiceCollection Services { get; }

        public INodeSnapshotPoint Build()
        {
            Services.AddSingleton<IServiceSnapshot, ServiceSnapshot>();
            Services.AddSingleton<IValueResolverProvider, ValueResolverProvider>();
            Services.AddSingleton<ISourceInjector, SourceInjector>();
            Services.AddSingleton<INodeMerger, NodeMerger>();
           
            Services.AddSingleton<INodeSnapshotPointProvider, NodeSnapshotPointProvider>();
            return Services.BuildServiceProvider().GetRequiredService<INodeSnapshotPointProvider>().Get();
        }
    }
}
