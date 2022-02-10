using System;
using System.Collections.Generic;
using System.Threading;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class ResolverProxy
    {
        private int _indexMax = -1;
        private readonly ThreadLocal<Resolver> _tlResolver;
        public ResolverProxy(Type serviceType)
        {
            _tlResolver = new ThreadLocal<Resolver>(() => new Resolver(_indexMax, typeof(IEnumerable<>).MakeGenericType(serviceType), serviceType));
        }
        internal int IncrementIndex()
        {
            _indexMax++;
            return _indexMax;
        }
        public int Count { get { return _indexMax + 1; } }

        internal object GetValue(IServiceProvider provider, int requestIndex)
        {
            return _tlResolver.Value.GetValue(provider, requestIndex);
        }
    }

}
