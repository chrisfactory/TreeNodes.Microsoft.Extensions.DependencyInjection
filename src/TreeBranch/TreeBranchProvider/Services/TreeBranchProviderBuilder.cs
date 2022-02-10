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

        public ITreeBranch Build()
        {
            Services.AddSingleton<IServiceCollectionSnapshot, ServiceCollectionSnapshot>();
            Services.AddSingleton<IServiceValueResolver, ServiceValueResolver>();

            Services.AddSingleton<ISnapshotBranchMergerService, SnapshotBranchMergerService>();
            Services.AddSingleton<ITreeBranch, TreeBranchProvider>();
            return Services.BuildServiceProvider().GetRequiredService<ITreeBranch>();//.Get();
        }
    }
}
