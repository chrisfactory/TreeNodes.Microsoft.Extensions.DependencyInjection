using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Test.TestServices
{
    internal class NodeSnapshotPointKake : INodeSnapshotPoint
    {
        public string Key => throw new System.NotImplementedException();

        public void ConnectTo(params IServiceCollection[] sources)
        {
            throw new System.NotImplementedException();
        }

        public ServiceCollection CreateBranch()
        {
            throw new System.NotImplementedException();
        }
    }
}
