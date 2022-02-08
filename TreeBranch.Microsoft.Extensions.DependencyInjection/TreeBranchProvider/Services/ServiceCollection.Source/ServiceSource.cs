using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceSource: IServiceSource
    {
        public ServiceSource(IServiceCollection services)
        {
            Source = services;
        }
        public IServiceCollection Source { get; }
    }
}
