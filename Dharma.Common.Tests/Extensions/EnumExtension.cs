using Dharma.Common.Extensions;
using Dharma.Common.Tests.Extensions.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dharma.Common.Tests.Extensions
{
    [TestClass]
    public class EnumExtension
    {
        [TestMethod]
        public void Description_With_Value_Return_Value()
        {
            var option3 = DescriptionEnum.Option3.Description();

            Assert.AreEqual(option3, "Option 3");
        }

        [TestMethod]
        public void Description_With_Empty_Return_Empty()
        {
            var option2 = DescriptionEnum.Option2.Description();

            Assert.AreEqual(option2, string.Empty);
        }

        [TestMethod]
        public void Description_Not_Have_Attribute_Return_Null()
        {
            var option1 = DescriptionEnum.Option1.Description();

            Assert.AreEqual(option1, "Option1");
        }
    }
}
