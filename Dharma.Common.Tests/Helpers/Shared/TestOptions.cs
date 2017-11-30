using Dharma.Common.Configurations;

namespace Dharma.Common.Tests.Helpers.Shared
{
    public class TestOptions : BaseOptions
    {
        public string OptionString { get; set; }

        public int OptionInteger { get; set; }

        public bool OptionBoolean { get; set; }

        public double OptionDouble { get; set; }

        public override string Name()
        {
            return "Test";
        }
    }
}
