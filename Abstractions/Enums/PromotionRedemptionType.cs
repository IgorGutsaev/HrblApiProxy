using System.ComponentModel;

namespace Filuet.Hrbl.Ordering.Abstractions.Enums
{
    public enum PromotionRedemptionType
    {
        [Description("AUTOMATIC")]
        Automatic = 0x01,
        [Description("OPTIONAL")]
        Optional,
        [Description("MIX")]
        Mix
    }
}
