using Filuet.Infrastructure.Attributes;

namespace Filuet.Hrbl.Ordering.Abstractions.Enums
{
    public enum ActionLevel
    {
        [Code("Info")]
        Info = 0x01,
        [Code("Warning")]
        Warning,
        [Code("Error")]
        Error
    }
}
