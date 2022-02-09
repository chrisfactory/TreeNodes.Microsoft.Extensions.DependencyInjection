using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface IValueResolver
    {
        ServiceDescriptor Descriptor { get; }
        bool IsService { get; }
        object Get(object arg);
    }
}
