using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class SourceInjector : ISourceInjector
    {
        public SourceInjector(IServiceSource source, INodeSnapshotPoint node)
        {
            source.Source.AddSingleton(node);
            Source = source.Source;
        }

        public IServiceCollection Source { get;  }
    }
}
