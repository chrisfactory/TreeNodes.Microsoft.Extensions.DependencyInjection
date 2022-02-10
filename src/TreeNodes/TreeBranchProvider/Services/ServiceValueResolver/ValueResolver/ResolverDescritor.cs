using Microsoft.Extensions.DependencyInjection;
using System;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class ResolverDescritor
    {
        private readonly int _index;
        private readonly ResolverProxy _proxy;
        private readonly ServiceDescriptor _Descriptor;
        private readonly bool _IsService;
        public ResolverDescritor(ServiceDescriptor descriptor, ResolverProxy proxy, bool isService)
        {
            _Descriptor = descriptor;
            _IsService = isService;
            _index = proxy.IncrementIndex();
            _proxy = proxy;
        }

        internal IValueResolver CreateResolver(IServiceProvider provider)
        {
            if (!_IsService)
                return new WithoutValueResolver(_Descriptor, _IsService);

            if (_proxy.Count == 1)
                return new DirectValueResolver(provider, _Descriptor, _IsService);

            return new ConditionalValueResolver(_index, _proxy, provider, _Descriptor, _IsService);
        }
    }
}
