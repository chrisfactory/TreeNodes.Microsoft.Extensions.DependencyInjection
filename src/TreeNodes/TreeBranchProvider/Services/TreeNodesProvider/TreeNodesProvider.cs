using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class TreeNodesProvider : ITreeNodeProvider
    {
        private readonly INodeMerger _merger;
        public TreeNodesProvider(INodeMerger merger)
        {
            _merger = merger;
        }

        public string Key { get; }

        public ServiceCollection CreateNode()
        {
            return _merger.MergeNodeTo(new ServiceCollection());
        }
          
        public void MergeNodeTo(params IServiceCollection[] sources)
        {
            foreach (var source in sources)
                _merger.MergeNodeTo(source);
        }
    }
}
