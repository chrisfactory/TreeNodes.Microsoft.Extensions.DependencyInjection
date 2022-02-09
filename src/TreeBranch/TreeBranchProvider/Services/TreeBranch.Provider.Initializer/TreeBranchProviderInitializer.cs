namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class TreeBranchProviderInitializer : ITreeBranchProviderInitializer
    {
        private readonly ITreeBranchProvider _treeBranchProvider;
        private readonly IRefreshServiceCollectionSource _refreshSourceService;
        public TreeBranchProviderInitializer(ITreeBranchProvider treeBranchProvider, IRefreshServiceCollectionSource refreshSourceService)
        {
            _treeBranchProvider = treeBranchProvider;
            _refreshSourceService = refreshSourceService;
        }
        public ITreeBranchProvider Get()
        {
            Initialize();           
            return _treeBranchProvider;
        }
        private void Initialize()
        {
            _refreshSourceService.Crushed(_treeBranchProvider);
        }

    }
}
