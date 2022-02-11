using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class SourceInjector : ISourceInjector
    {
        public SourceInjector(IServiceSource source, INodeSnapshotPoint node)
        {
            source.Source.AddSingleton(node);
            Source = source.Source;
        }

        public IServiceCollection Source { get; }
    }
}
