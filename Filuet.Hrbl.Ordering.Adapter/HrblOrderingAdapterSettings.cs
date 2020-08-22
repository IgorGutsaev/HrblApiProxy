using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapterSettings
    {
        public string ApiUri { get; internal set;}
        public string Consumer { get; internal set; }
        public string Login { get; internal set; }
        public string Password { get; internal set; }
    }
}
