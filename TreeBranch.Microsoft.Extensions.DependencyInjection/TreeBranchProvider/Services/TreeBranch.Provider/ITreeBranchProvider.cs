using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    public interface ITreeBranchProvider
    {
        ServiceCollection CreateNewServicesFromBranch();
        void MergeTo(params IServiceCollection[] sources);
    }
}
