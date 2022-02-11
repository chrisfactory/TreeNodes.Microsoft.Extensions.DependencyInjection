using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class NodeSnapshotPointProvider : INodeSnapshotPointProvider
    {
        private readonly IInternalNodeSnapshotPoint _node;
        public NodeSnapshotPointProvider(IInternalNodeSnapshotPoint node, INodeMerger merger, IServiceKey key)
        {
            _node = node;
            _node.Initialize(merger, key);
        }
        public INodeSnapshotPoint Get()
        {
            return _node;
        }
    }
}
