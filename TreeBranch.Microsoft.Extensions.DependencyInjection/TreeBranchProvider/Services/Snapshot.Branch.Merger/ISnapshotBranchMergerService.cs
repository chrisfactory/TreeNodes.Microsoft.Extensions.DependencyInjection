using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface ISnapshotBranchMergerService
    {
        TServiceCollection MergeBranchTo<TServiceCollection>(TServiceCollection to) where TServiceCollection : IServiceCollection;
    }
}
