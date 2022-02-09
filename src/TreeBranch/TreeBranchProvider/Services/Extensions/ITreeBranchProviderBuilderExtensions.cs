using Microsoft.Extensions.DependencyInjection; 
namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal static class ITreeBranchProviderBuilderExtensions
    {
        public static ITreeBranchProviderBuilder UseSource(this ITreeBranchProviderBuilder builder,IServiceCollection source)
        {
            builder.Services.AddSingleton<IServiceSource>(new ServiceSource(source));
            return builder;
        }
    }
}
