using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Filuet.Hrbl.Ordering.Common;
using Filuet.Hrbl.Ordering.Abstractions.Dto;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using System.Text.Json;

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
        public async Task<SkuInventory[]> GetSkuAvailabilityAsync(string warehouse, Dictionary<string, int> items)
            => await Task.FromResult(items.Select(x => new SkuInventory { Sku = x.Key, AvailableQuantity = x.Value + 1 }).ToArray());

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="sku">sku to request</param>
        /// <param name="quantity"></param>
        public async Task<SkuInventory> GetSkuAvailabilityAsync(string warehouse, string sku, int quantity)
            => await Task.FromResult(new SkuInventory { Sku = sku, AvailableQuantity = quantity++ });

        public Task<InventoryItem[]> GetProductInventory(string country, string orderType = null)
            => null;

        public Task<CatalogItem[]> GetProductCatalog(string country, string orderType = null)
            => null;
        #endregion

        #region Distributor
        public async Task<(bool isValid, string memberId)> ValidateSsoBearerToken(string token)
            => await Task.FromResult((true, "7918180560"));

        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        public async Task<DistributorProfile> GetProfile(string distributorId)
            => await Task.FromResult(JsonSerializer.Deserialize<DistributorProfile>(Properties.Resources.MockProfileResponse.ResolveHrblMess()));

        public async Task UpdateAddressAndContacts(Action<ProfileUpdateBuilder> setup) { await Task.FromResult(0); /* Sort of a success */ }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country)
            => await Task.FromResult(JsonSerializer.Deserialize<FOPPurchasingLimitsResult>(Properties.Resources.MockDSFOPPurchasingLimit.ResolveHrblMess()));

        public async Task<TinDetails> GetDistributorTins(string distributorId, string country)
            => await Task.FromResult(JsonSerializer.Deserialize<GetDistributorTinsResult>(Properties.Resources.MockMemberTins.ResolveHrblMess()).TinDetails);

        public async Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null)
            => await Task.FromResult(JsonSerializer.Deserialize<DistributorVolumePointsDetailsResult>(Properties.Resources.MockVP.Replace("yyyy/MM", month.ToString("yyyy/MM"))).DistributorVolumeDetails.DistributorVolume);

        public async Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country)
            => await Task.FromResult(JsonSerializer.Deserialize<DistributorDiscountResult>(Properties.Resources.MockDSFOPPurchasingLimit));

        /// <summary>
        /// Get member cash limit
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country">Shipp to country</param>
        /// <returns></returns>
        public async Task<DsCashLimitResult> GetDsCashLimit(string distributorId, string country)
            => await Task.FromResult(JsonSerializer.Deserialize<DsCashLimitResult>(Properties.Resources.MockDSCashLimit));

        public async Task<PricingResponse> GetPriceDetails(Action<PricingRequestBuilder> setupAction)
            => await Task.FromResult(JsonSerializer.Deserialize<PricingResponse>(Properties.Resources.MockPricingResponse));

        public async Task<PricingResponse> GetPriceDetails(PricingRequest request)
            => await Task.FromResult(JsonSerializer.Deserialize<PricingResponse>(Properties.Resources.MockPricingResponse));

        public async Task<string> HpsPaymentGateway(HpsPaymentPayload payload)
            => await Task.FromResult(Guid.NewGuid().ToString());

        public async Task<SubmitResponse> SubmitOrder(Action<SubmitRequestBuilder> setupAction)
            => await Task.FromResult(new SubmitResponse { OrderStatus = "SUCCESS", OrderNumber = "LRK0123456" });

        public async Task<SubmitResponse> SubmitOrder(SubmitRequest request)
            => await Task.FromResult(new SubmitResponse { OrderStatus = "SUCCESS", OrderNumber = "LRK0123456" });
        #endregion

        #region Common
        public async Task<bool> GetOrderDualMonthStatus(string country)
            => await Task.FromResult(true);

        public Task<DsPostamatDetails[]> GetPostamats(string country, string postamatType, string region = null, string city = null, string zipCode = null)
            => null;

        public Task<WHFreightCode[]> GetShippingWhseAndFreightCodes(string postalCode, bool expressDeliveryFlag = true)
            => null;

        public Task<ConversionRateResponse> GetConversionRate(ConversionRateRequest request)
            => null;

        public Task<GetDSEligiblePromoSKUResponseDTO> GetDSEligiblePromoSKU(GetDSEligiblePromoSKURequestDTO request)
            => null;

        public Task<PollResult> PollRequest()
            => null;

        public Task<SsoAuthResult> GetSsoProfileAsync(string login, string password)
            => null;
        #endregion
    }
}