using Microsoft.Extensions.DependencyInjection; 
namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal static class ITreeNodeProviderBuilderExtensions
    {
        internal static ITreeNodeProviderBuilder UseSource(this ITreeNodeProviderBuilder builder,IServiceCollection source)
        {
            builder.Services.AddSingleton<IServiceSource>(new ServiceSource(source));
            return builder;
        }
    }
}
