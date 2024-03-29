﻿using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{
    internal interface INodeMerger
    {
        TServiceCollection MergeNodeTo<TServiceCollection>(TServiceCollection to, INodeSnapshotPoint context) where TServiceCollection : IServiceCollection;
    }
}
