using System;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceKey : IServiceKey
    {
        public ServiceKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            Key = key;
        }
        public string Key { get; }
    }
}
