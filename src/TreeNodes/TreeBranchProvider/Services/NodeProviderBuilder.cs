using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class NodeProviderBuilder : INodeProviderBuilder
    {
        public NodeProviderBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public INodeSnapshotPoint Build()
        {
            Services.AddSingleton<IServiceSnapshot, ServiceSnapshot>();
            Services.AddSingleton<IServiceValueResolver, ServiceValueResolver>();

            Services.AddSingleton<INodeMerger, NodeMerger>();
            Services.AddSingleton<INodeSnapshotPoint, NodeSnapshotPoint>();
            return Services.BuildServiceProvider().GetRequiredService<INodeSnapshotPoint>();//.Get();
        }
    }
}
