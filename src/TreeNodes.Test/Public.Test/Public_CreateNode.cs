using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
using TreeNodes.Test;
namespace TreeNodes.Public.Test
{
    [TestClass]
    public class Public_CreateNode
    {

        [TestMethod($"{nameof(IServiceCollectionExtensions)}.{nameof(IServiceCollectionExtensions.CreateNode)}")]
        public void CreateNode()
        {
            string nodeName = "test.singleton.services";

            var servicesBuilder = new ServiceCollection()
                         .AddSingleton<IService, CommonService1>()
                         .AddTransient<IService, CommonService2>()
                         .AddSingleton<IService>(p => new CommonService3());
            var node = servicesBuilder.CreateNode(nodeName);

            Assert.AreEqual(nodeName, node.Key);
            Assert.AreEqual(4, servicesBuilder.Count);
            Assert.AreEqual(typeof(IService), servicesBuilder[0].ServiceType);
            Assert.AreEqual(typeof(IService), servicesBuilder[1].ServiceType);
            Assert.AreEqual(typeof(IService), servicesBuilder[2].ServiceType);
            Assert.AreEqual(typeof(INodeSnapshotPoint), servicesBuilder[3].ServiceType);
            Assert.AreEqual(typeof(CommonService1), servicesBuilder[0].GetImplementationType());
            Assert.AreEqual(typeof(CommonService2), servicesBuilder[1].GetImplementationType());
            Assert.AreEqual(typeof(IService), servicesBuilder[2].GetImplementationType());
            Assert.AreEqual(typeof(NodeSnapshotPoint), servicesBuilder[3].GetImplementationType());
        }





        public interface IService { }
        private class CommonService1 : IService
        {
            public static int ResolveCount = 0;
            public CommonService1()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }
        private class CommonService2 : IService
        {
            public static int ResolveCount = 0;
            public CommonService2()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }
        private class CommonService3 : IService
        {
            public static int ResolveCount = 0;
            public CommonService3()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }

    }
}