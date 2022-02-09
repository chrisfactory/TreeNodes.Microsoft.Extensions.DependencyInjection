using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{
    internal interface IServiceValueResolver : IEnumerable<IValueResolver>
    { 
    }
}
