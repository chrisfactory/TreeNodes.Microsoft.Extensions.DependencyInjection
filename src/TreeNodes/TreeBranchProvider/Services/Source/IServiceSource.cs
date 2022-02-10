using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface IServiceSource
    {
        IServiceCollection Source { get; }
    }
}
