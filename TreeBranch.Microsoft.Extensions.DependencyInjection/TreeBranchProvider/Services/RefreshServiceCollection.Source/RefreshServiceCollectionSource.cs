namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class RefreshServiceCollectionSource : IRefreshServiceCollectionSource
    {
        private readonly IServiceSource _serviceSource;
        public RefreshServiceCollectionSource(IServiceSource serviceSource, ITreeBranchProvider treeBranchProvider)
        {
            _serviceSource = serviceSource;
        }
        public void Crushed(ITreeBranchProvider treeBranchProvider)
        {
            lock (_serviceSource.Source)
            {
                _serviceSource.Source.Clear();
                treeBranchProvider.MergeTo(_serviceSource.Source);
            }
        }
    }
}
