using Microsoft.Extensions.DependencyInjection; 
namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal static class ITreeNodeProviderBuilderExtensions
    {
        internal static INodeProviderBuilder UseSource(this INodeProviderBuilder builder,IServiceCollection source)
        {
            builder.Services.AddSingleton<IServiceSource>(new ServiceSource(source));
            return builder;
        }
    }
}
