using System;
using System.Collections.Generic;
using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceProviderExtensions
    {
        public static INodeSnapshotPoint GetNode(this IServiceProvider provider, string nodeKey)
        {
            return new SnapshotPointStorage(provider.GetRequiredService<IEnumerable<INodeSnapshotPoint>>()).Get(nodeKey);
        }
    }
}
