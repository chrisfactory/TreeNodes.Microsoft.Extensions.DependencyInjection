using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface INodeProviderBuilder
    {
        IServiceCollection Services { get; }
        INodeSnapshotPoint Build();
    }
}
