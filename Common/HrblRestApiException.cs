using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Filuet.Hrbl.Ordering.Common
{
    public class HrblRestApiException : Exception
    {
        public HrblRestApiException(string message)
            :base(message)
        {

        }
    }
}