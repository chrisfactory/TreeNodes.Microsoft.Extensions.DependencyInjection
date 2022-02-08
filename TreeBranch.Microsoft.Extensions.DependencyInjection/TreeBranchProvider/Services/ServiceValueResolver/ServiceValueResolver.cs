using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceValueResolver : IServiceValueResolver
    {
        private readonly IReadOnlyCollection<IValueResolver> _resolver;
        public ServiceValueResolver(IServiceCollectionSnapshot snapshot)
        {
            _resolver = InitializeResolver(snapshot);
        }

        private static IReadOnlyCollection<IValueResolver> InitializeResolver(IServiceCollectionSnapshot snapshot)
        {
            var checkService = snapshot.Provider.GetRequiredService<IServiceProviderIsService>();
            var resolver = new List<ValueResolver>();
            var _dic = new Dictionary<Type, ResolverProxy>();
            foreach (var servicesDescriptor in snapshot.Services)
            {
                if (!_dic.ContainsKey(servicesDescriptor.ServiceType))
                    _dic.Add(servicesDescriptor.ServiceType, new ResolverProxy(servicesDescriptor.ServiceType, snapshot.Provider));
                resolver.Add(new ValueResolver(servicesDescriptor, _dic[servicesDescriptor.ServiceType], checkService.IsService(servicesDescriptor.ServiceType)));
            } 
            return resolver;
        }

        public IEnumerator<IValueResolver> GetEnumerator() => _resolver.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_resolver).GetEnumerator();
    }

    internal interface IValueResolver
    {
        ServiceDescriptor Descriptor { get; }
        bool IsService { get; }
        object Get(object arg);
    }
    internal class ValueResolver : IValueResolver
    {
        private readonly int _index;
        private readonly ResolverProxy _proxy;
        internal ValueResolver(ServiceDescriptor descriptor, ResolverProxy proxy, bool isService)
        {
            Descriptor = descriptor;
            IsService = isService;
            _index = proxy.IncrementIndex();
            _proxy = proxy;
        }
         
        public ServiceDescriptor Descriptor { get; }
        public bool IsService { get; }
        public object Get(object arg)
        {
            return _proxy.GetValue(_index);
        }
    }

    internal class ResolverProxy
    {
        private readonly IServiceProvider _provider;

        private readonly Type _IEnumerableT;
        private readonly Type _ServiceType;
        public ResolverProxy(Type serviceType, IServiceProvider provider)
        {
            _ServiceType = serviceType;
            _IEnumerableT = typeof(IEnumerable<>).MakeGenericType(serviceType);
            _provider = provider;
        }

        private int _index = -1;
        internal int IncrementIndex()
        {
            _index++;
            return _index;
        }

        private IReadOnlyList<object>? _values;
        internal object GetValue(int requestIndex)
        {
            if (_index != requestIndex && requestIndex == 0)
                _values = ((IEnumerable<object>)_provider.GetRequiredService(_IEnumerableT)).ToArray();
            if (_values != null)
            {
                var result = _values[requestIndex];
                if ((_values.Count - 1) == requestIndex)
                    _values = null;
                return result;
            } 
            return _provider.GetRequiredService(_ServiceType);
        }
    }
}

