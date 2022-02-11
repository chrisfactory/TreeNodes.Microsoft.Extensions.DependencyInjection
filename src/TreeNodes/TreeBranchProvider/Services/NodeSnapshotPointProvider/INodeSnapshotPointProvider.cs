using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface INodeSnapshotPointProvider
    {
        INodeSnapshotPoint Get();
    }
}
