using Microsoft.Extensions.DependencyInjection;
using System;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class WithoutValueResolver : ValueResolverBase
    {
        internal WithoutValueResolver(ServiceDescriptor descriptor, bool isService) : base(descriptor, isService)
        {
        }

        public override object Get(object arg)
        {
            throw new InvalidOperationException("Invalide resolver");
        }
    }
}
