using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class TreeBranchProviderBuilder : ITreeBranchProviderBuilder
    {
        public TreeBranchProviderBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public ITreeBranchProvider Build()
        {
            Services.AddSingleton<IServiceCollectionSnapshot, ServiceCollectionSnapshot>();
            Services.AddSingleton<ISnapshotBranchMergerService, SnapshotBranchMergerService>();
            Services.AddSingleton<IRefreshServiceCollectionSource, RefreshServiceCollectionSource>();
            Services.AddSingleton<ITreeBranchProviderInitializer, TreeBranchProviderInitializer>(); 
            Services.AddSingleton<ITreeBranchProvider, TreeBranchProvider>();
            return Services.BuildServiceProvider().GetRequiredService<ITreeBranchProviderInitializer>().Get();
        }
    }
}
