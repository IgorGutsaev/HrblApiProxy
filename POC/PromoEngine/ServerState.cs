using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using System.Collections.Generic;

namespace Filuet.Hrbl.Ordering.POC.PromoEngine
{
    public static class ServerState
    {
        public static Dictionary<string, List<Promotion>> PromotionTests { get; set; } = new Dictionary<string, List<Promotion>>();
        public static Dictionary<string, PricingResponse> PricingResponses { get; set; } = new Dictionary<string, PricingResponse>();

        public static DataSource DataSource
        {
            get
            {
                DataSource result = _source;

                if (_source != PromoEngine.DataSource.Original)
                    _source = PromoEngine.DataSource.Original;

                return result;
            }
            set { _source = value; }
        }

        private static DataSource _source = DataSource.Original;
    }

    public enum DataSource
    {
        Original = 0x01,
        Cached,
        Mock
    }
}