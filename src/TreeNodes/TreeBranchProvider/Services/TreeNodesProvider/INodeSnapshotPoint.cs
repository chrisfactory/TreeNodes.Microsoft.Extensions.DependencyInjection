using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{

    public interface INodeSnapshotPoint
    {
        public string Key { get;}
        ServiceCollection CreateBranch();
        void InsertBranchStack(params IServiceCollection[] sources);
    }
}
