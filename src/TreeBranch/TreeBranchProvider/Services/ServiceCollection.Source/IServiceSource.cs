using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface IServiceSource
    {
        IServiceCollection Source { get; }
    }
}
