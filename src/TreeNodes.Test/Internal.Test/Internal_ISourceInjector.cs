using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
using TreeNodes.Test.TestServices;

namespace TreeNodes.Internal.Test
{
    [TestClass]
    public class Internal_ISourceInjector
    {
        private IServiceCollection testServices = new ServiceCollection();
        public Internal_ISourceInjector()
        {
            testServices.AddSingleton<INodeSnapshotPoint, NodeSnapshotPointKake>();
            testServices.AddSingleton<IServiceSource, ServiceSource>();
            testServices.AddSingleton<ISourceInjector, SourceInjector>();
        }
        [TestMethod(nameof(ISourceInjector))]
        public void PreservationSource()
        {
            IServiceCollection source = new ServiceCollection();
            testServices.AddSingleton(source); 
            var testProvider = testServices.BuildServiceProvider();
            var sourceService = testProvider.GetService<ISourceInjector>();
            Assert.IsNotNull(sourceService);
            Assert.IsNotNull(sourceService.Source);
            Assert.AreEqual(source, sourceService.Source);
            Assert.AreNotEqual(testServices, sourceService.Source);
            Assert.AreEqual(1, source.Count);
            Assert.AreEqual(typeof(INodeSnapshotPoint), source[0].ServiceType); 
        }
    }
}