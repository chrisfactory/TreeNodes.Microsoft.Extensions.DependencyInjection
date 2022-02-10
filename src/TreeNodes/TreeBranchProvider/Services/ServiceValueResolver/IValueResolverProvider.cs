using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface IValueResolverProvider : IEnumerable<IValueResolver>
    { 
    }
}
