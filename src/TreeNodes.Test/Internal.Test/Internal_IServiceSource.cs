using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeNodes.Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Internal.Test
{
    [TestClass]
    public class Internal_IServiceSource
    {
        private IServiceCollection testServices = new ServiceCollection();
        public Internal_IServiceSource()
        {
            testServices.AddSingleton<IServiceSource, ServiceSource>();
        }
        [TestMethod(nameof(IServiceSource))]
        public void PreservationSource()
        {
            IServiceCollection source = new ServiceCollection();
            testServices.AddSingleton(source);
            var testProvider = testServices.BuildServiceProvider();
            var sourceService = testProvider.GetService<IServiceSource>();
            Assert.IsNotNull(sourceService);
            Assert.IsNotNull(sourceService.Source);
            Assert.AreEqual(source, sourceService.Source);
            Assert.AreNotEqual(testServices, sourceService.Source);
        }
    }
}