using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Newtonsoft.Json;
using Filuet.Hrbl.Ordering.Common;
using Filuet.Hrbl.Ordering.Abstractions.Dto;
using Filuet.Hrbl.Ordering.Abstractions.Models;

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
            => await Task.FromResult(JsonConvert.DeserializeObject<DistributorProfile>(Properties.Resources.MockProfileResponse
                , new HrblNullableResponseConverter<DistributorProfile>()));

        public async Task UpdateAddressAndContacts(Action<ProfileUpdateBuilder> setup) { await Task.FromResult(0); /* Sort of a success */ }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country)
            => await Task.FromResult(JsonConvert.DeserializeObject<FOPPurchasingLimitsResult>(Properties.Resources.MockDSFOPPurchasingLimit
                , new HrblNullableResponseConverter<FOPPurchasingLimitsResult>()));

        public async Task<TinDetails> GetDistributorTins(string distributorId, string country)
            => await Task.FromResult(JsonConvert.DeserializeObject<GetDistributorTinsResult>(Properties.Resources.MockMemberTins
                , new HrblNullableResponseConverter<GetDistributorTinsResult>()).TinDetails);

        public async Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null)
            => await Task.FromResult(JsonConvert.DeserializeObject<DistributorVolumePointsDetailsResult>(Properties.Resources.MockVP.Replace("yyyy/MM", month.ToString("yyyy/MM"))).DistributorVolumeDetails.DistributorVolume);

        public async Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country)
            => await Task.FromResult(JsonConvert.DeserializeObject<DistributorDiscountResult>(Properties.Resources.MockDSFOPPurchasingLimit));

        /// <summary>
        /// Get member cash limit
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country">Shipp to country</param>
        /// <returns></returns>
        public async Task<DsCashLimitResult> GetDsCashLimit(string distributorId, string country)
            => await Task.FromResult(JsonConvert.DeserializeObject<DsCashLimitResult>(Properties.Resources.MockDSCashLimit));

        public async Task<PricingResponse> GetPriceDetails(Action<PricingRequestBuilder> setupAction)
            => await Task.FromResult(JsonConvert.DeserializeObject<PricingResponse>(Properties.Resources.MockPricingResponse));

        public async Task<PricingResponse> GetPriceDetails(PricingRequest request)
            => await Task.FromResult(JsonConvert.DeserializeObject<PricingResponse>(Properties.Resources.MockPricingResponse));

        public async Task<string> HpsPaymentGateway(HpsPaymentPayload payload)
            => await Task.FromResult(Guid.NewGuid().ToString());

        public async Task<SubmitResponse> SubmitOrder(Action<SubmitRequestBuilder> setupAction)
            => await Task.FromResult(new SubmitResponse { OrderStatus = "SUCCESS", OrderNumber = "LRK0123456" });

        public async Task<SubmitResponse> SubmitOrder(SubmitRequest request)
            => await Task.FromResult(new SubmitResponse { OrderStatus = "SUCCESS", OrderNumber = "LRK0123456" });
        #endregion

        #region Common
        public async Task<bool> GetOrderDualMonthStatus(string country) => await Task.FromResult(true);

        public Task<DsPostamatDetails[]> GetPostamats(string country, string postamatType, string region = null, string city = null, string zipCode = null)
        {
            throw new NotImplementedException();
        }

        public Task<WHFreightCode[]> GetShippingWhseAndFreightCodes(string postalCode, bool expressDeliveryFlag = true)
        {
            throw new NotImplementedException();
        }

        public Task<ConversionRateResponse> GetConversionRate(ConversionRateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetDSEligiblePromoSKUResponseDTO> GetDSEligiblePromoSKU(GetDSEligiblePromoSKURequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<PollResult> PollRequest()
        {
            throw new NotImplementedException();
        }

        public Task<SsoAuthResult> GetSsoProfileAsync(string login, string password)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}