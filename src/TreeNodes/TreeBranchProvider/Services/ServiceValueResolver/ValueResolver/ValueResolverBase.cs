using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal abstract class ValueResolverBase : IValueResolver
    {
        internal ValueResolverBase(ServiceDescriptor descriptor, bool isService)
        {
            Descriptor = descriptor;
            IsService = isService; 
        }

        public ServiceDescriptor Descriptor { get; }
        public bool IsService { get; }
        public abstract object Get(object arg);
    } 
}
