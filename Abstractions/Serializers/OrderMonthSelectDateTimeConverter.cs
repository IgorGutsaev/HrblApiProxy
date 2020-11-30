using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Serializers
{
    public class OrderMonthSelectDateTimeConverter : IsoDateTimeConverter
    {
        public OrderMonthSelectDateTimeConverter()
        {
            base.DateTimeFormat = "yyMM";
        }
    }
}
