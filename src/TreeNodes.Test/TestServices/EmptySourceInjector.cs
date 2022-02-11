using Microsoft.Extensions.DependencyInjection;
using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Test.TestServices
{
    internal class EmptySourceInjector : ISourceInjector
    {
        public EmptySourceInjector(IServiceSource source)
        {
            Source = source.Source;
        }
        public IServiceCollection Source { get; }
    }
}
