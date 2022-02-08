using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{ 
    internal class TreeBranchProvider : ITreeBranchProvider
    {
        private readonly ISnapshotBranchMergerService _merger;
        public TreeBranchProvider(ISnapshotBranchMergerService merger)
        {
            _merger = merger; 
        }
        public ServiceCollection CreateNewServicesFromBranch()
        {
            var services = new ServiceCollection();
            _merger.MergeBranchTo(services);
            return services;
        } 
        public void MergeTo(params IServiceCollection[] sources)
        {
            foreach (var source in sources)
                _merger.MergeBranchTo(source);
        }
    }
}
