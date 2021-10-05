using Filuet.Hrbl.Ordering.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public interface IHrblOrderingAdapter
    {
        Task<(bool isValid, string memberId)> ValidateSsoBearerToken(string token);

        Task<SkuInventory[]> GetSkuAvailability(string warehouse, Dictionary<string, int> items);

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="sku">sku to request</param>
        /// <param name="quantity"></param>
        Task<SkuInventory> GetSkuAvailability(string warehouse, string sku, int quantity);

        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        Task<DistributorProfile> GetProfile(string distributorId);

        Task UpdateAddressAndContacts(Action<ProfileUpdateBuilder> setupAction);

        Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country);

        Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null);

        Task<bool> GetOrderDualMonthStatus(string country);

        Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country);

        Task<DsCashLimitResult> GetDsCashLimit(string distributorId, string country);

        Task<InventoryItem[]> GetProductInventory(string country, string orderType = null);

        Task<CatalogItem[]> GetProductCatalog(string country, string orderType = null);

        Task<DsPostamatDetails[]> GetPostamats(string country, string postamatType, string region = null, string city = null, string zipCode = null);

        Task<WHFreightCode[]> GetShippingWhseAndFreightCodes(string postalCode, bool expressDeliveryFlag = true);

        Task<PricingResponse> GetPriceDetails(Action<PricingRequestBuilder> setupAction);

        Task<PricingResponse> GetPriceDetails(PricingRequest request);

        Task<string> HpsPaymentGateway(HpsPaymentPayload payload);

        Task<SubmitResponse> SubmitOrder(Action<SubmitRequestBuilder> setupAction);

        Task<SubmitResponse> SubmitOrder(SubmitRequest request);
    }
}