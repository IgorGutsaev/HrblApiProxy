using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Serializers
{
    public class StandardDateTimeConverter : IsoDateTimeConverter
    {
        public StandardDateTimeConverter()
        {
            base.DateTimeFormat = "s";
        }
    }
}
