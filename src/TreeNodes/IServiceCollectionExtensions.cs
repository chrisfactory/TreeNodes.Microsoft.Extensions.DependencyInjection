using System;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for manage TreeNodes services.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        public static ITreeNodeProvider AddNodeDescriptor(this IServiceCollection source)
        {
            return source.AddNodeDescriptor(Guid.NewGuid().ToString());
        }
        public static ITreeNodeProvider AddNodeDescriptor(this IServiceCollection source, string name)
        {
            return new TreeNodeProviderBuilder().UseSource(source).Build();
        }
    }
}