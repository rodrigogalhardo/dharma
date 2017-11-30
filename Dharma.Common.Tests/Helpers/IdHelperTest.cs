using Dharma.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dharma.Common.Tests.Helpers
{
    [TestClass]
    public class IdHelperTest
    {
        private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [TestMethod]
        public void Make_Success()
        {
            var result = IdHelper.Make();

            Assert.IsTrue(result.Length == 24);

            foreach (var item in result)
            {
                Assert.IsTrue(chars.IndexOf(item) != -1);
            }

            result = IdHelper.Make(48);

            Assert.IsTrue(result.Length == 48);

            foreach (var item in result)
            {
                Assert.IsTrue(chars.IndexOf(item) != -1);
            }

            result = IdHelper.Make(1);

            Assert.IsTrue(result.Length == 1);

            foreach (var item in result)
            {
                Assert.IsTrue(chars.IndexOf(item) != -1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Make_Less_Than_Zero_Throw_Exception()
        {
            IdHelper.Make(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Make_More_Than_48_Throw_Exception()
        {
            IdHelper.Make(49);
        }
    }
}
