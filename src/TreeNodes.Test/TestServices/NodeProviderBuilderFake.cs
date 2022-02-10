using Microsoft.Extensions.DependencyInjection;
using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Test.TestServices
{
    internal class NodeProviderBuilderFake : INodeProviderBuilder
    {
        public NodeProviderBuilderFake()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public INodeSnapshotPoint Build()
        {
            return Services.BuildServiceProvider().GetRequiredService<INodeSnapshotPoint>();//.Get();
        }
    }
}
