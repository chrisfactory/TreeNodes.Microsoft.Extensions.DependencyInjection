using System;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for manage TreeNodes services.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        public static INodeSnapshotPoint CreateNode(this IServiceCollection source)
        {
            return source.CreateNode(Guid.NewGuid().ToString());
        }
        public static INodeSnapshotPoint CreateNode(this IServiceCollection source, string key)
        {
            return new NodeProviderBuilder().AddSource(source).AddKey(key).Build(); ;
        }
    }
}