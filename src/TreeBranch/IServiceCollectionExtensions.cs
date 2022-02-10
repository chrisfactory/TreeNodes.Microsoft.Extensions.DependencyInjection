using System;
using TreeBranch.Microsoft.Extensions.DependencyInjection;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for manage TreeBranch services.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        public static ITreeBranch CreateBranch(this IServiceCollection source)
        {
            return source.CreateBranch(Guid.NewGuid().ToString());
        }
        public static ITreeBranch CreateBranch(this IServiceCollection source, string name)
        {
            return new TreeBranchProviderBuilder().UseSource(source).Build();
        }
    }
}