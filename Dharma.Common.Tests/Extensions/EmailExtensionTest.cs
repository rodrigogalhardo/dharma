using Dharma.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dharma.Common.Tests.Extensions
{
    [TestClass]
    public class EmailExtensionTest
    {
        [DataTestMethod]
        [DataRow("a@dotz.com")]
        [DataRow("a@dotz.com.br")]
        [DataRow("teste@dotz.com.br")]
        public void IsValidEmail_Valid_Return_True(string email)
        {
            Assert.IsTrue(email.IsEmail());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("a")]
        [DataRow("a@")]
        [DataRow("@")]
        [DataRow("@a")]
        [DataRow("@a.")]
        [DataRow("@.")]
        [DataRow("@a.com")]
        [DataRow("a@a")]
        public void IsValidEmail_Invalid_Return_False(string email)
        {
            Assert.IsFalse(email.IsEmail());
        }
    }
}