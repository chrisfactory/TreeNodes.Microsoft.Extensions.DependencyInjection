using Microsoft.Extensions.DependencyInjection;
using System;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class DirectValueResolver : ValueResolverBase
    {
        private readonly IServiceProvider _provider;
        internal DirectValueResolver(IServiceProvider provider, ServiceDescriptor descriptor, bool isService) : base(descriptor, isService)
        {
            _provider = provider;
        }

        public override object Get(object arg)
        {
            return _provider.GetRequiredService(base.Descriptor.ServiceType);
        }
    }
}