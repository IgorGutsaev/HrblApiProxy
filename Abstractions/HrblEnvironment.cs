using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public enum HrblEnvironment
    {
        Unknown = 0x01,
        Prod,
        PRS,
        TS3,
        Mock
    }
}
