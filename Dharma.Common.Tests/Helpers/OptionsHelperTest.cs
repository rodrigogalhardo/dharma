using Dharma.Common.Helpers;
using Dharma.Common.Tests.Helpers.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dharma.Common.Tests.Helpers
{
    [TestClass]
    public class OptionsHelperTest
    {
        protected static IConfigurationRoot Configuration { get; private set; }

        static OptionsHelperTest()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [TestMethod]
        public void GetSection_Success()
        {
            var teste = OptionsHelper.GetSection<TestOptions>(Configuration);

            Assert.AreEqual("string", teste["OptionString"]);
            Assert.AreEqual("999", teste["OptionInteger"]);
            Assert.AreEqual("True", teste["OptionBoolean"]);
            Assert.AreEqual("99.1", teste["OptionDouble"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSection_Null_Throw_Exception()
        {
            OptionsHelper.GetSection<TestOptions>(null);
        }

        [TestMethod]
        public void GetConfiguration_Success()
        {
            var teste = OptionsHelper.GetConfiguration<TestOptions>(Configuration);

            Assert.AreEqual("string", teste.OptionString);
            Assert.AreEqual(999, teste.OptionInteger);
            Assert.AreEqual(true, teste.OptionBoolean);
            Assert.AreEqual(99.1, teste.OptionDouble);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetConfiguration_Null_Throw_Exception()
        {
            OptionsHelper.GetConfiguration<TestOptions>(null);
        }
    }
}