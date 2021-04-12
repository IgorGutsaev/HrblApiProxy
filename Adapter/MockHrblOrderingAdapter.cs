using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Newtonsoft.Json;
using Filuet.Hrbl.Ordering.Common;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class MockHrblOrderingAdapter : IHrblOrderingAdapter
    {
        public HrblEnvironment Environment => HrblEnvironment.Mock;

        #region Inventory
        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="items">collection of goods identifier</param>
        public async Task<SkuInventory[]> GetSkuAvailability(string warehouse, Dictionary<string, uint> items)
            => items.Select(x => new SkuInventory { Sku = x.Key, AvailableQuantity = x.Value + 1 }).ToArray();

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="sku">sku to request</param>
        /// <param name="quantity"></param>
        public async Task<SkuInventory> GetSkuAvailability(string warehouse, string sku, uint quantity)
            => new SkuInventory { Sku = sku, AvailableQuantity = quantity++ };

        public async Task<InventoryItem[]> GetProductInventory(string country, string orderType = null)
            => null;

        public async Task<CatalogItem[]> GetProductCatalog(string country, string orderType = null)
            => null;
        #endregion

        #region Distributor
        public async Task<(bool isValid, string memberId)> ValidateSsoBearerToken(string token)
            => (true, "7918180560");

        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        public async Task<DistributorProfile> GetProfile(string distributorId)
            => JsonConvert.DeserializeObject<DistributorProfile>(Properties.Resources.MockProfileResponse
                , new HrblNullableResponseConverter<DistributorProfile>());

        public async Task UpdateAddressAndContacts(Action<ProfileUpdateBuilder> setup) { /* Sort of a success */ }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country)
            => JsonConvert.DeserializeObject<FOPPurchasingLimitsResult>(Properties.Resources.MockDSFOPPurchasingLimit
                , new HrblNullableResponseConverter<FOPPurchasingLimitsResult>());

        public async Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null)
            => JsonConvert.DeserializeObject<DistributorVolumePointsDetailsResult>(Properties.Resources.MockVP.Replace("yyyy/MM", month.ToString("yyyy/MM"))).DistributorVolumeDetails.DistributorVolume;

        public async Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country)
            => JsonConvert.DeserializeObject<DistributorDiscountResult>(Properties.Resources.MockDSFOPPurchasingLimit);

        /// <summary>
        /// Get member cash limit
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country">Shipp to country</param>
        /// <returns></returns>
        public async Task<DsCashLimitResult> GetDsCashLimit(string distributorId, string country)
            => JsonConvert.DeserializeObject<DsCashLimitResult>(Properties.Resources.MockDSCashLimit);

        public async Task<PricingResponse> GetPriceDetails(Action<PricingRequestBuilder> setupAction)
            => JsonConvert.DeserializeObject<PricingResponse>(Properties.Resources.MockPricingResponse);

        public async Task<PricingResponse> GetPriceDetails(PricingRequest request)
            => JsonConvert.DeserializeObject<PricingResponse>(Properties.Resources.MockPricingResponse);

        public async Task<string> HpsPaymentGateway(HpsPaymentPayload payload)
            => Guid.NewGuid().ToString();

        public async Task<string> SubmitOrder(Action<SubmitRequestBuilder> setupAction)
            => "LRK0123456";

        public async Task<string> SubmitOrder(SubmitRequest request)
            => "LRK0123456";
        #endregion

        #region Common
        public async Task<bool> GetOrderDualMonthStatus(string country) => true;

        public async Task<DsPostamatDetails[]> GetPostamats(string country, string postamatType, string region = null, string city = null, string zipCode = null)
            => null;

        public async Task<WHFreightCode[]> GetShippingWhseAndFreightCodes(string postalCode, bool expressDeliveryFlag = true) => null;
        #endregion
    }
}
