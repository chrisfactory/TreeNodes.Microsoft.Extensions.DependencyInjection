using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class Resolver
    {
        private readonly int _indexMax = -1;
        private readonly Type _IEnumerableT, _ServiceType;
        internal Resolver(int indexMax, Type enumeraableType, Type serviceType)
        {
            _indexMax = indexMax;
            _IEnumerableT = enumeraableType;
            _ServiceType = serviceType;

        }
        private IReadOnlyList<object>? _values;
        internal object GetValue(IServiceProvider provider, int requestIndex)
        {
            if (_indexMax != requestIndex && requestIndex == 0)
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
