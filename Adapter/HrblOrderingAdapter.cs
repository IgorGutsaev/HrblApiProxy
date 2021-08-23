using Filuet.Fusion.SDK;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Filuet.Hrbl.Ordering.Abstractions;
using System.Text;
using Filuet.Hrbl.Ordering.Common;
using Filuet.Hrbl.Ordering.Abstractions.Builders;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapter : IHrblOrderingAdapter
    {
        /// <summary>
        /// Hrbl auto-generated proxy for REST API
        /// </summary>
        private readonly HLOnlineOrderingRS _proxy;
        private readonly HrblOrderingAdapterSettings _settings;

        public HrblEnvironment Environment => _settings.ApiUri.ToLower().Contains("/ts3/") ? HrblEnvironment.TS3 :
            (_settings.ApiUri.ToLower().Contains("/prs/") ? HrblEnvironment.PRS :
            (_settings.ApiUri.ToLower().Contains("/prod/") ? HrblEnvironment.Prod : HrblEnvironment.Unknown));

        public HrblOrderingAdapter(HrblOrderingAdapterSettings settings)
        {
            _settings = settings;
            _proxy = new HLOnlineOrderingRS(new Uri(_settings.ApiUri));
            _proxy.SerializationSettings = null;
            _proxy.DeserializationSettings = null;
            _proxy.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.Login}:{ _settings.Password}")));
        }

        #region Inventory
        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="items">collection of goods identifier</param>
        public async Task<SkuInventory[]> GetSkuAvailability(string warehouse, Dictionary<string, uint> items)
        {
            if (!items.Any())
                return new SkuInventory[0];

            if (string.IsNullOrWhiteSpace(warehouse))
                throw new ArgumentException("Warehouse must be specified");

            object response = await _proxy.GetSkuAvailability.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                SkuInquiryDetails = items.Select(x => new
                {
                    Sku = new
                    {
                        SkuName = x.Key,
                        Quantity = x.Value.ToString(),
                        WarehouseCode = warehouse
                    }
                }).ToList()
            });

            return JsonConvert.DeserializeObject<SkuInventoryDetailsResult>(JsonConvert.SerializeObject(response)).SkuInventoryDetails.Inventory;
        }

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="sku">sku to request</param>
        /// <param name="quantity"></param>
        public async Task<SkuInventory> GetSkuAvailability(string warehouse, string sku, uint quantity)
            => await GetSkuAvailability(warehouse, new List<KeyValuePair<string, uint>> { new KeyValuePair<string, uint>(sku, quantity) }
                .ToDictionary(x => x.Key, x => x.Value)).ContinueWith(x => x.Result.FirstOrDefault());

        public async Task<InventoryItem[]> GetProductInventory(string country, string orderType = null)
        {
            OrderType type = string.IsNullOrWhiteSpace(orderType) ? OrderType.Rso
                : EnumHelper.GetValueFromDescription<OrderType>(orderType);

            object response = await _proxy.GetProductInventory.POSTAsync(new
            {
                CountryCode = country,
                OrderType = EnumHelper.GetDescription(type)
            });

            InventoryResult result = JsonConvert.DeserializeObject<InventoryResult>(JsonConvert.SerializeObject(response));
            if (!result.Inventory.IsSussess)
                throw new HrblRestApiException($"An error occured while requesting product inventory in {country}");

            return result.Inventory.Inventories.ItemsRoot.Items;
        }

        public async Task<CatalogItem[]> GetProductCatalog(string country, string orderType = null)
        {
            OrderType type = string.IsNullOrWhiteSpace(orderType) ? OrderType.Rso
                : EnumHelper.GetValueFromDescription<OrderType>(orderType);

            object response = await _proxy.GetProductCatalog.POSTAsync(new
            {
                CountryCode = country,
                OrderType = EnumHelper.GetDescription(type)
            });

            return JsonConvert.DeserializeObject<CatalogResult>(JsonConvert.SerializeObject(response)).CatalogDetails.Items;
        }
        #endregion

        #region Distributor
        public async Task<(bool isValid, string memberId)> ValidateSsoBearerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Country is mandatory");

            object response = await _proxy.ApiValidate.POSTAsync(
                ApiValidateRequest.Create(_settings.OrganizationId, token));

            ApiValidateResponse result = JsonConvert.DeserializeObject<ApiValidateResponse>(JsonConvert.SerializeObject(response));

            return (result.IsValid, result.MemberId);
        }

        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        public async Task<DistributorProfile> GetProfile(string distributorId)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID must be specified");

            object response = await _proxy.GetDistributorProfile.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
            });

            return JsonConvert.DeserializeObject<DistributorProfileResult>(response.ToString(),
                new HrblNullableResponseConverter<DistributorProfileResult>()).Profile;
        }

        public async Task UpdateAddressAndContacts(Action<ProfileUpdateBuilder> setup)
        {
            UpdateAddressAndContactsRequest request =
                setup.CreateTargetAndInvoke().SetServiceConsumer(_settings.Consumer).Build();

            DistributorProfile profile = await GetProfile(request.DistributorId);

            if (request.Address != null)
            {
                DistributorAddress addressToUpdate = profile.Shipping?.Addresses?.FirstOrDefault(x => x.Type.Equals(request.Address.Type, StringComparison.InvariantCultureIgnoreCase));
                if (addressToUpdate != null)
                    request.Address.FillInWithUnspecifiedData(addressToUpdate);
                else request.Address = null; // We're not allowed to create new address
            }

            if (request.Contact != null)
            {
                DistributorContact contactToUpdate = profile.Shipping?.Contacts?.FirstOrDefault(x => x.Type.Equals(request.Contact.Type, StringComparison.InvariantCultureIgnoreCase));
                if (contactToUpdate != null)
                    request.Contact.FillInWithUnspecifiedData(contactToUpdate);
                else request.Contact = null; // We're not allowed to create new contact
            }

            object response = await _proxy.UpdateDsAddressContacts.POSTAsync(request);
        }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID is mandatory");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is mandatory");

            object response = await _proxy.DSFOPPurchasingLimits.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorID = distributorId,
                CountryCode = country.ToUpper()
            });

            return JsonConvert.DeserializeObject<FOPPurchasingLimitsResult>(JsonConvert.SerializeObject(response)
                , new HrblNullableResponseConverter<FOPPurchasingLimitsResult>());
        }

        public async Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID must be specified");

            object response = await _proxy.GetDistributorVolumePoints.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
                FromMonth = month.ToString("yyyy/MM"),
                ToMonth = monthTo.HasValue ? monthTo.Value.ToString("yyyy/MM") : month.ToString("yyyy/MM"),
                IncludeORgVolumes = "N"
            });

            return JsonConvert.DeserializeObject<DistributorVolumePointsDetailsResult>(JsonConvert.SerializeObject(response),
                new HrblNullableResponseConverter<DistributorVolumePointsDetailsResult>()).DistributorVolumeDetails.DistributorVolume;
        }

        public async Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country)
        {
            object response = await _proxy.GetDistributorDiscount.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
                OrderMonth = month.ToString("yyyy/MM"),
                ShipToCountry = country
            });

            return JsonConvert.DeserializeObject<DistributorDiscountResult>(JsonConvert.SerializeObject(response),
                new HrblNullableResponseConverter<DistributorDiscountResult>());
        }

        /// <summary>
        /// Get member cash limit
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country">Shipp to country</param>
        /// <returns></returns>
        public async Task<DsCashLimitResult> GetDsCashLimit(string distributorId, string country)
        {
            object response = await _proxy.DsCashLimit.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
                ShipToCountry = country,
                PaymentMethod = "CASH"
            });

            return JsonConvert.DeserializeObject<DsCashLimitResult>(JsonConvert.SerializeObject(response));
        }

        public async Task<PricingResponse> GetPriceDetails(Action<PricingRequestBuilder> setupAction)
            => await GetPriceDetails(setupAction.CreateTargetAndInvoke().AddServiceConsumer(_settings.Consumer).Build());

        public async Task<PricingResponse> GetPriceDetails(PricingRequest request)
        {
            request.ServiceConsumer = _settings.Consumer;

            object response = await _proxy.GetPriceDetails.POSTAsync(request);
                
            return JsonConvert.DeserializeObject<PricingResponse>(JsonConvert.SerializeObject(response),
                new HrblNullableResponseConverter<PricingResponse>());
        }

        public async Task<string> HpsPaymentGateway(HpsPaymentPayload payload)
        {
            HpsPaymentRequest request = new HpsPaymentRequestBuilder()
                .AddServiceConsumer(_settings.Consumer)
                .AddPayload(payload).Build();

            HpsPaymentResponse result =
                JsonConvert.DeserializeObject<HpsPaymentResponse>(JsonConvert.SerializeObject(await _proxy.HPSPaymentGateway.POSTAsync(request)));

            if (result.Errors.HasErrors)
                throw new HrblRestApiException(result.Errors.ErrorMessage);

            return result.PaymentResponse.TransactionID;
        }

        public async Task<SubmitResponse> SubmitOrder(Action<SubmitRequestBuilder> setupAction)
        {
            SubmitRequest request = setupAction.CreateTargetAndInvoke()
                .AddServiceConsumer(_settings.Consumer)
                .Build();

            object response = await _proxy.SubmitOrder.POSTAsync(request);

            return JsonConvert.DeserializeObject<SubmitResponse>(JsonConvert.SerializeObject(response));
        }

        public async Task<SubmitResponse> SubmitOrder(SubmitRequest request)
        {
            request.ServiceConsumer = _settings.Consumer;

            object response = await _proxy.SubmitOrder.POSTAsync(request);

            return JsonConvert.DeserializeObject<SubmitResponse>(JsonConvert.SerializeObject(response));
        }
        #endregion

        #region Common
        public async Task<bool> GetOrderDualMonthStatus(string country)
        {
            if (string.IsNullOrWhiteSpace(country) || country.Trim().Length != 2)
                throw new ArgumentException("Country is mandatory");

            object response = await _proxy.GetOrderDualMonthStatus.POSTAsync(new
            {
                ShipToCountry = country.Trim().ToUpper()
            });

            return JsonConvert.DeserializeObject<OrderDualMonthStatus>(JsonConvert.SerializeObject(response)).IsDualMonthAllowed;
        }

        public async Task<DsPostamatDetails[]> GetPostamats(string country, string postamatType, string region = null, string city = null, string zipCode = null)
        {
            if (string.IsNullOrWhiteSpace(country) || country.Trim().Length != 2)
                throw new ArgumentException("Country is mandatory");

            if (string.IsNullOrWhiteSpace(postamatType))
                throw new ArgumentException("Postamat is mandatory");

            object response = await _proxy.GetDSPostamatDetails.POSTAsync(new
            {
                Country = country.Trim(),
                PostamatType = postamatType.Trim(),
                City = city?.Trim(),
                Region = region?.Trim(),
                ZipCode = zipCode?.Trim()
            });

            DSPostamatDetailsResult result = JsonConvert.DeserializeObject<DSPostamatDetailsResult>(JsonConvert.SerializeObject(response));

            if (result.Errors.HasErrors)
                throw new HrblRestApiException(result.Errors.ErrorMessage);

            return result.DsPostamatDetails;
        }

        public async Task<WHFreightCode[]> GetShippingWhseAndFreightCodes(string postalCode, bool expressDeliveryFlag = true)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code is mandatory");

            object response = await _proxy.GetShippingWhseAndFreightCodes.POSTAsync(new
            {
                ServiceConsumer = _settings.Consumer,
                ExpressDeliveryFlag = expressDeliveryFlag ? "Y" : "N",
                PostalCode = postalCode.Trim()
            });

            WHFreightCodesResult result = JsonConvert.DeserializeObject<WHFreightCodesResult>(JsonConvert.SerializeObject(response));

            if (result.ErrorCode != "0")
                throw new HrblRestApiException(result.ErrorMessage);

            return result.WHFriehtCodes;
        }
        #endregion

        public override string ToString() => Environment.ToString();
    }
}
