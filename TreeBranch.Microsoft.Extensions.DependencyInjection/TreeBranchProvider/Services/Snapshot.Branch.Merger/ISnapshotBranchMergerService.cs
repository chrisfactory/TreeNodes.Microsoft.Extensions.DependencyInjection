using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface ISnapshotBranchMergerService
    {
        void MergeBranchTo(IServiceCollection to);
    }
}
