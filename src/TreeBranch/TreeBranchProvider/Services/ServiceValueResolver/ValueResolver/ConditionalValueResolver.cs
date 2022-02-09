using Microsoft.Extensions.DependencyInjection;
using System;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class ConditionalValueResolver : ValueResolverBase
    {
        private readonly IServiceProvider _provider;
        private readonly int _index;
        private readonly ResolverProxy _proxy;
        internal ConditionalValueResolver(int index, ResolverProxy proxy, IServiceProvider provider, ServiceDescriptor descriptor, bool isService) : base(descriptor, isService)
        {
            _provider = provider;
            _index = index;
            _proxy = proxy;
        }

        public override object Get(object arg)
        {
            return _proxy.GetValue(_provider, _index);
        }
    }
}
