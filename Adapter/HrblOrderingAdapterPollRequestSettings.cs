using System;
using System.Collections.Generic;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapterPollRequestSettings
    {
        public IEnumerable<(string distributorId, DateTime month)> Input_for_GetVolumePoints { get; set; }

        public IEnumerable<(string distributorId, string country)> Input_for_GetDistributorTIN { get; set; }

        public IEnumerable<string> Input_for_GetProfile { get; set; }

        public IEnumerable<(string distributorId, DateTime month, string country)> Input_for_GetDistributorDiscount { get; set; }

        public IEnumerable<(string sku, string warehouse)> Input_for_GetSku { get; set; }

        public IEnumerable<string> Input_for_GetProductInventory { get; set; }

        public IEnumerable<(string distributorId, string country)> Input_for_GetDSFOPPurchasingLimits { get; set; }

        public IEnumerable<(string distributorId, string country)> Input_for_GetCashLimit { get; set; }

        public IEnumerable<string> Input_for_GetDualMonth { get; set; }

        public IEnumerable<(string exchangeRateType, string fromCurrency, string toCurrency)> Input_for_GetConversationRate { get; set; }

        public IEnumerable<string> Input_for_GetPricingRequests { get; set; }
    }
}
