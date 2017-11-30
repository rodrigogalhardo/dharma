using Dharma.Common.Helpers;
using Dharma.Common.Tests;
using Dharma.Data.MongoDB.Providers;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dharma.Data.MongoDB.Tests.Providers
{
    [TestClass]
    public class MongoDatabaseProviderTest : BaseTest
    {
        private MongoOptions _options;

        public MongoDatabaseProviderTest()
        {
            _options = OptionsHelper.GetConfiguration<MongoOptions>(Configuration);
        }

        [TestMethod]
        public void Create_Success()
        {
            var provider = new MongoDatabaseProvider(Options.Create(_options));

            var client = provider.Create();

            Assert.AreEqual(client.DatabaseNamespace.DatabaseName, _options.DatabaseName);
        }
    }
}
