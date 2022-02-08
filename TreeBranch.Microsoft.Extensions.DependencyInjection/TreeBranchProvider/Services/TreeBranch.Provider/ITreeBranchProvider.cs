using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    public interface ITreeBranchProvider
    {
        ServiceCollection CreateNewServicesFromBranche();
        void MergeTo(params IServiceCollection[] sources);
    }
}
