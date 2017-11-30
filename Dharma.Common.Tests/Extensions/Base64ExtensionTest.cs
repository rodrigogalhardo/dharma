using Dharma.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dharma.Common.Tests.Extensions
{
    [TestClass]
    public class Base64ExtensionTest
    {
        [TestMethod]
        public void IsBase64_True()
        {
            string value = "dGVzdGU=";

            Assert.IsTrue(value.IsBase64());
        }

        [TestMethod]
        public void IsBase64_False_With_Null_Value()
        {
            string value = null;

            Assert.IsFalse(value.IsBase64());
        }

        [TestMethod]
        public void IsBase64_False_With_Empty_Value()
        {
            string value = string.Empty;

            Assert.IsFalse(value.IsBase64());
        }

        [TestMethod]
        public void IsBase64_False_With_White_Space_Value()
        {
            string value = " ";

            Assert.IsFalse(value.IsBase64());
        }

        [TestMethod]
        public void IsBase64_False_With_Text_Value()
        {
            string value = "teste";

            Assert.IsFalse(value.IsBase64());
        }

        [TestMethod]
        public void ToBase64_Success()
        {
            string value = "teste";

            Assert.AreEqual(value.ToBase64(), "dGVzdGU=");
        }

        [TestMethod]
        public void ToBase64_Null()
        {
            string value = null;

            Assert.AreEqual(value.ToBase64(), null);
        }

        [TestMethod]
        public void ToBase64_Base64()
        {
            string value = "dGVzdGU=";

            Assert.AreEqual(value.ToBase64(), "ZEdWemRHVT0=");
        }

        [TestMethod]
        public void FromBase64_Success()
        {
            string value = "dGVzdGU=";

            Assert.AreEqual(value.FromBase64(), "teste");
        }

        [TestMethod]
        public void FromBase64_Null()
        {
            string value = null;

            Assert.AreEqual(value.FromBase64(), null);
        }

        [TestMethod]
        public void FromBase64_Base64()
        {
            string value = "ZEdWemRHVT0=";

            Assert.AreEqual(value.FromBase64(), "dGVzdGU=");
        }
    }
}
