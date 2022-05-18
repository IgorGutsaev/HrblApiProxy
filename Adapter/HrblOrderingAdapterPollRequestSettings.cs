using System;
using System.Collections.Generic;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapterPollRequestSettings
    {
        public IEnumerable<(string distributorId, DateTime month)> Input_for_GetVolumePoints { get; set; }
    }
}
