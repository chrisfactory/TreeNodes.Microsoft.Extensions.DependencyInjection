using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface ITreeBranchProviderBuilder
    {
        IServiceCollection Services { get; }
        ITreeBranchProvider Build();
    }
}
