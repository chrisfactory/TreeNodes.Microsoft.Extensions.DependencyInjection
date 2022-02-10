using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class TreeNodeProviderBuilder : ITreeNodeProviderBuilder
    {
        public TreeNodeProviderBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public ITreeNodeProvider Build()
        {
            Services.AddSingleton<IServiceSnapshot, ServiceSnapshot>();
            Services.AddSingleton<IServiceValueResolver, ServiceValueResolver>();

            Services.AddSingleton<INodeMerger, NodeMerger>();
            Services.AddSingleton<ITreeNodeProvider, TreeNodesProvider>();
            return Services.BuildServiceProvider().GetRequiredService<ITreeNodeProvider>();//.Get();
        }
    }
}
