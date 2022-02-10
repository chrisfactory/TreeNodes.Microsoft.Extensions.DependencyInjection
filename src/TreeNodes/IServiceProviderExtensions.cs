using System;
using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceProviderExtensions
    {
        public static INodeSnapshotPoint GetNode(this IServiceProvider provider, string nodeKey)
        {
            return provider.GetRequiredService<ISnapshotPointStorage>().Get(nodeKey);
        }
    }
}
