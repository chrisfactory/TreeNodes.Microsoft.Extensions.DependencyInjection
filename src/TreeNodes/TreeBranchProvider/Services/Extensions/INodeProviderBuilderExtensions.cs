using Microsoft.Extensions.DependencyInjection;
namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal static class INodeProviderBuilderExtensions
    {
        internal static INodeProviderBuilder AddSource(this INodeProviderBuilder builder, IServiceCollection source)
        {
            builder.Services.AddSingleton<IServiceSource>(new ServiceSource(source));
            return builder;
        }
        internal static INodeProviderBuilder AddKey(this INodeProviderBuilder builder, string key)
        {
            builder.Services.AddSingleton<IServiceKey>(new ServiceKey(key));
            return builder;
        }
    }
}
