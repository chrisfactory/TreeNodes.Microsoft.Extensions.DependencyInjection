using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class NodeSnapshotPoint : INodeSnapshotPoint
    {
        private readonly INodeMerger _merger;
        public NodeSnapshotPoint(INodeMerger merger, IServiceKey key)
        {
            _merger = merger;
            Key = key.Key;
        }

        public string Key { get; }

        public ServiceCollection CreateBranch()
        {
            return _merger.MergeNodeTo(new ServiceCollection(), this);
        }

        public void ConnectTo(params IServiceCollection[] sources)
        {
            foreach (var source in sources)
                _merger.MergeNodeTo(source, this);
        }

        internal IServiceCollection Combine(INodeSnapshotPoint b)
        {
            var s = CreateBranch();
            b.ConnectTo(s);
            return s;
        }
    }
}
