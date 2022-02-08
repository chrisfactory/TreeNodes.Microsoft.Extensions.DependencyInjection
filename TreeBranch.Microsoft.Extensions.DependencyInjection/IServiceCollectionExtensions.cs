using TreeBranch.Microsoft.Extensions.DependencyInjection; 
namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static ITreeBranchProvider CreateBranchProvider(this IServiceCollection source)
        {
            return new TreeBranchProviderBuilder().UseSource(source).Build();
        }
    }
}