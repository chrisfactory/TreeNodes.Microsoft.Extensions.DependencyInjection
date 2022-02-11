using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
using TreeNodes.Test.TestServices;

namespace TreeNodes.Internal.Test
{
    [TestClass]
    public class Internal_IServiceSnapshot
    {
        private IServiceCollection testServices = new ServiceCollection();
        public Internal_IServiceSnapshot()
        {
            testServices.AddSingleton<ISourceInjector, EmptySourceInjector>();
            testServices.AddSingleton<IServiceSource, ServiceSource>();
            testServices.AddSingleton<IServiceSnapshot, ServiceSnapshot>();
        }
        [TestMethod(nameof(IServiceSnapshot))]
        public void SnapshotTest()
        {
            IServiceCollection source = new ServiceCollection();

            testServices.AddSingleton(source);
            source.AddSingleton<IEmptyTestService, EmptyTestService>();
            source.AddSingleton<IEmptyTestService, EmptyTestService>();


            var testProvider = testServices.BuildServiceProvider();
            var snapshotService = testProvider.GetService<IServiceSnapshot>();

            for (int i = 0; i < 10; i++)
                source.AddSingleton<IEmptyTestService, EmptyTestService>();


            Assert.IsNotNull(snapshotService);
            Assert.IsNotNull(snapshotService.Services);
            Assert.IsNotNull(snapshotService.Provider);
            Assert.AreEqual(2, snapshotService.Services.Count);

            var resolvedServices = snapshotService.Provider.GetServices<IEmptyTestService>();
            Assert.IsNotNull(resolvedServices);
            Assert.AreEqual(2, resolvedServices.Count());

            //Preservation
            Assert.AreEqual(12, source.Count());
        }
    }
}