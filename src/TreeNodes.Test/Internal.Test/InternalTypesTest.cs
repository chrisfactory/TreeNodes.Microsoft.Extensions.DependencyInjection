using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Test.Types
{
    [TestClass]
    public class InternalTypesTest
    {
        [TestMethod]
        [DataRow(typeof(INodeProviderBuilder))]
        [DataRow(typeof(NodeProviderBuilder))] 
        [DataRow(typeof(NodeSnapshotPoint))]
        [DataRow(typeof(IServiceSource))] 
        [DataRow(typeof(ServiceSource))]
        [DataRow(typeof(IServiceSnapshot))]
        [DataRow(typeof(ServiceSnapshot))]
        [DataRow(typeof(INodeMerger))]
        [DataRow(typeof(NodeMerger))]
        [DataRow(typeof(INodeProviderBuilderExtensions))]
        [DataRow(typeof(IServiceValueResolver))]
        [DataRow(typeof(ServiceValueResolver))]
        [DataRow(typeof(WithoutValueResolver))]
        [DataRow(typeof(ValueResolverBase))]
        [DataRow(typeof(ResolverProxy))]
        [DataRow(typeof(ResolverDescritor))]
        [DataRow(typeof(Resolver))]
        [DataRow(typeof(IValueResolver))]
        [DataRow(typeof(DirectValueResolver))]
        [DataRow(typeof(ConditionalValueResolver))]
        public void AreInternal(Type t )
        {
            Assert.IsTrue(TestType.IsInternal(t));
        }

        [TestMethod]
        [DataRow(typeof(INodeSnapshotPoint))]
        [DataRow(typeof(IServiceCollectionExtensions))]
        public void ArePublic(Type t)
        {
            Assert.IsTrue(TestType.IsPublic(t));
        }
    }
}
