using Microsoft.Extensions.DependencyInjection;
using System;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class NodeSnapshotPoint : IInternalNodeSnapshotPoint
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private INodeMerger _merger;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private bool _initialized = false;
        public void Initialize(INodeMerger merger, IServiceKey key)
        {
            _merger = merger;
            Key = key.Key;
            _initialized = true;
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Key { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public ServiceCollection CreateBranch()
        {
            CheckAccess();
            return _merger.MergeNodeTo(new ServiceCollection(), (INodeSnapshotPoint)this);
        }



        public void ConnectTo(params IServiceCollection[] sources)
        {
            CheckAccess();
            foreach (var source in sources)
                _merger.MergeNodeTo(source, (INodeSnapshotPoint)this);
        }

        internal IServiceCollection Combine(INodeSnapshotPoint b)
        {
            CheckAccess();
            var s = CreateBranch();
            b.ConnectTo(s);
            return s;
        }
        private void CheckAccess()
        {
            if (!_initialized)
                throw new InvalidOperationException(nameof(CheckAccess));
        }

    }
}
