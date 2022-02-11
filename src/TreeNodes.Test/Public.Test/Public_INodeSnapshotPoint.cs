using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
using TreeNodes.Test;
namespace TreeNodes.Public.Test
{
    [TestClass]
    public class Public_INodeSnapshotPoint
    {
        [TestMethod($"{nameof(INodeSnapshotPoint)}.{nameof(INodeSnapshotPoint.CreateBranch)}")]
        public void CreateBranch()
        {
            string nodeName = "test.singleton.services";

            var servicesBuilder = new ServiceCollection()
                         .AddSingleton<IService, CommonService1>()
                         .AddTransient<IService, CommonService2>()
                         .AddSingleton<IService>(p => new CommonService3());
            var newBranche = servicesBuilder.CreateNode(nodeName).CreateBranch();
            Assert.IsNotNull(newBranche);
            Assert.AreEqual(typeof(ServiceCollection), newBranche.GetType());
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
        [TestMethod($"{nameof(INodeSnapshotPoint)}.{nameof(INodeSnapshotPoint.CreateBranch)}.Build")]
        public void BuildBranche()
        {
            string nodeName = "test.singleton.services";

            var servicesBuilder = new ServiceCollection()
                         .AddSingleton<IService, CommonService1>()
                         .AddTransient<IService, CommonService2>()
                         .AddSingleton<IService>(p => new CommonService3());
            var newBranche = servicesBuilder.CreateNode(nodeName).CreateBranch();
            var provider = newBranche.BuildServiceProvider();
            Assert.IsNotNull(provider);

        }

        [TestMethod($"{nameof(INodeSnapshotPoint)}.{nameof(INodeSnapshotPoint.CreateBranch)}.GetServices")]
        public void GetServices()
        {
            string nodeName = "test.singleton.services";

            var servicesBuilder = new ServiceCollection()
                         .AddSingleton<IService, CommonService1>()
                         .AddTransient<IService, CommonService2>()
                         .AddSingleton<IService>(p => new CommonService3());
            var newBranche = servicesBuilder.CreateNode(nodeName).CreateBranch();
            var provider = newBranche.BuildServiceProvider();
            var services = provider.GetServices<IService>();
            Assert.IsNotNull(services);
            Assert.AreEqual(3, services.Count()); 
        }

        [TestMethod($"{nameof(INodeSnapshotPoint)}.{nameof(INodeSnapshotPoint.CreateBranch)}.GetService")]
        public void GetService()
        {
            string nodeName = "test.singleton.services";

            var servicesBuilder = new ServiceCollection()
                         .AddSingleton<IService, CommonService1>()
                         .AddTransient<IService, CommonService2>()
                         .AddSingleton<IService>(p => new CommonService3());
            var newBranche = servicesBuilder.CreateNode(nodeName).CreateBranch();
            var provider = newBranche.BuildServiceProvider();
            var services = provider.GetService<IEnumerable<IService>>();
            Assert.IsNotNull(services);
            Assert.AreEqual(3, services.Count());
        }

        [TestMethod($"{nameof(INodeSnapshotPoint)}.{nameof(INodeSnapshotPoint.CreateBranch)}.LifeTime.Singleton")]
        public void LifeTimeSingleton()
        {
            string nodeName = "test.services";

            var servicesBuilder = new ServiceCollection()
                         .AddSingleton<IService, LifeTime1>() 
                         .AddSingleton<IService>(p => new LifeTime2());
            var newBranche = servicesBuilder.CreateNode(nodeName).CreateBranch();
            for (int i = 0; i < 10; i++)
            { 
                var provider = newBranche.BuildServiceProvider();
                var services = provider.GetService<IEnumerable<IService>>();
                Assert.IsNotNull(services);
                Assert.AreEqual(2, services.Count());
            }
            Assert.AreEqual(1, LifeTime1.ResolveCount);
            Assert.AreEqual(1, LifeTime2.ResolveCount);
        }


        [TestMethod($"{nameof(INodeSnapshotPoint)}.{nameof(INodeSnapshotPoint.CreateBranch)}.LifeTime.Transient")]
        public void LifeTimeTransient()
        {
            string nodeName = "test.services";

            var servicesBuilder = new ServiceCollection()
                         .AddTransient<IService, LifeTime3>()
                         .AddTransient<IService>(p => new LifeTime4());
            var newBranche = servicesBuilder.CreateNode(nodeName).CreateBranch();
            for (int i = 0; i < 10; i++)
            {
                var provider = newBranche.BuildServiceProvider();
                var services = provider.GetService<IEnumerable<IService>>();
                Assert.IsNotNull(services);
                Assert.AreEqual(2, services.Count());
            }
            Assert.AreEqual(10, LifeTime3.ResolveCount);
            Assert.AreEqual(10, LifeTime4.ResolveCount);
        }











        public interface IService { }
        private class CommonService1 : IService
        { 
        }
        private class CommonService2 : IService
        {
           
        }
        private class CommonService3 : IService
        { 
        }
        private class LifeTime1 : IService
        {
            public static int ResolveCount = 0;
            public LifeTime1()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }
        private class LifeTime2 : IService
        {
            public static int ResolveCount = 0;
            public LifeTime2()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }
        private class LifeTime3 : IService
        {
            public static int ResolveCount = 0;
            public LifeTime3()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }
        private class LifeTime4 : IService
        {
            public static int ResolveCount = 0;
            public LifeTime4()
            {
                var incremented_counter = Interlocked.Increment(ref ResolveCount);

            }
        }
    }
}