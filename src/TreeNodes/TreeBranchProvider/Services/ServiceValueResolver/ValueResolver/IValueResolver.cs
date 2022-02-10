using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface IValueResolver
    {
        ServiceDescriptor Descriptor { get; }
        bool IsService { get; }
        object Get(object arg);
    }
}
