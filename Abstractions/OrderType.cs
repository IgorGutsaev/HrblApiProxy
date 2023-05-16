using System.ComponentModel;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public enum OrderType
    {
        [Description("RSO")]
        Rso = 0x01,
        [Description("APF")]
        Apf,
        [Description("ETO")]
        Eto
    }
}