using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{

    public interface INodeSnapshotPoint
    {
        public string Key { get; }
        ServiceCollection CreateBranch();
        void ConnectTo(params IServiceCollection[] sources);


        public static IServiceCollection operator +(INodeSnapshotPoint a, INodeSnapshotPoint b)
        {
            return ((NodeSnapshotPoint)a).Combine(b);
        }
        public static IServiceCollection operator +(IServiceCollection b, INodeSnapshotPoint a) => a + b;

        public static IServiceCollection operator +(INodeSnapshotPoint a, IServiceCollection b)
        {
            a.ConnectTo(b);
            return b;
        }

    }

    internal interface IInternalNodeSnapshotPoint : INodeSnapshotPoint
    {
        void Initialize(INodeMerger merger, IServiceKey key);
    }
}
