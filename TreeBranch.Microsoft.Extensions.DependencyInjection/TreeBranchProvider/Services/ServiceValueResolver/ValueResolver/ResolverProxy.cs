using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class ResolverProxy
    {
        private int _index = -1;
        private readonly Type _IEnumerableT;
        private readonly Type _ServiceType;
        public ResolverProxy(Type serviceType)
        {
            _ServiceType = serviceType;
            _IEnumerableT = typeof(IEnumerable<>).MakeGenericType(serviceType);
        }
        internal int IncrementIndex()
        {
            _index++;
            return _index;
        }
        public int Count { get { return _index + 1; } }

        private IReadOnlyList<object>? _values;
        internal object GetValue(IServiceProvider provider, int requestIndex)
        {
            if (_index != requestIndex && requestIndex == 0)
                _values = ((IEnumerable<object>)provider.GetRequiredService(_IEnumerableT)).ToArray();
            if (_values != null)
            {
                var result = _values[requestIndex];
                if ((_values.Count - 1) == requestIndex)
                    _values = null;
                return result;
            }
            return provider.GetRequiredService(_ServiceType);
        }
    }
}
