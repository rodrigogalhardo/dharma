using System.ComponentModel;

namespace Dharma.Common.Tests.Extensions.Shared
{
    public enum DescriptionEnum
    {
        Option1,

        [Description]
        Option2,

        [Description("Option 3")]
        Option3
    }
}