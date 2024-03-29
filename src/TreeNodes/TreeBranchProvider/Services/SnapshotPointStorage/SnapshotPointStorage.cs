﻿using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal class SnapshotPointStorage 
    {
        private readonly IReadOnlyDictionary<string, INodeSnapshotPoint> _store;
        public SnapshotPointStorage(IEnumerable<INodeSnapshotPoint> nodes)
        {
            _store = nodes.ToDictionary(n => n.Key);
        }

        public INodeSnapshotPoint Get(string key)
        {
            return _store[key];
        } 
    }
}
