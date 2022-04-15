using System.ComponentModel;

namespace Filuet.Hrbl.Ordering.Abstractions.Enums
{
    public enum PromotionRedemptionLimit
    {
        [Description("ONE")]
        One = 0x01,
        [Description("MULTIPLE")]
        Multiple,
        [Description("ALL")]
        All
    }
}
