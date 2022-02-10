using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface ISnapshotPointStorage
    {
        INodeSnapshotPoint Get(string key);
    }
}
