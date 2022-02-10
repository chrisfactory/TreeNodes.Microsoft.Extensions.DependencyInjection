using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeNodes.Microsoft.Extensions.DependencyInjection;
using TreeNodes.Test.TestServices;

namespace TreeNodes.Test
{
    [TestClass]
    public class Internal_INodeProviderBuilderExtensions
    {
        private IServiceCollection testServices = new ServiceCollection();
        public Internal_INodeProviderBuilderExtensions()
        {
            testServices.AddSingleton<INodeProviderBuilder, NodeProviderBuilderFake>();
        }
        [TestMethod($"{nameof(INodeProviderBuilderExtensions)}.{nameof(INodeProviderBuilderExtensions.AddSource)}")]
        public void AddSourceExt()
        {
            IServiceCollection source = new ServiceCollection();

            testServices.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<INodeProviderBuilder>();
                builder.AddSource(source);
                return builder.Services.BuildServiceProvider().GetRequiredService<IServiceSource>();
            });

            var testProvider = testServices.BuildServiceProvider();
            var sourceService = testProvider.GetService<IServiceSource>();
            Assert.IsNotNull(sourceService);
            Assert.IsNotNull(sourceService.Source);
            Assert.AreEqual(source, sourceService.Source);
            Assert.AreNotEqual(testServices, sourceService.Source);
        }
    }
}