using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface ITreeNodeProviderBuilder
    {
        IServiceCollection Services { get; }
        ITreeNodeProvider Build();
    }
}
