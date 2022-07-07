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
using Filuet.Hrbl.Ordering.Abstractions.Dto;
using System.Net.Http;
using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapter : IHrblOrderingAdapter
    {
        /// <summary>
        /// Hrbl auto-generated proxy for REST API
        /// </summary>
        private readonly HLOnlineOrderingRS _proxy;
        private readonly HrblOrderingAdapterSettings _settings;
        private readonly ILogger<HrblOrderingAdapter> _logger;

        public HrblEnvironment Environment => _settings.ApiUri.ToLower().Contains("/ts3/") ? HrblEnvironment.TS3 :
            (_settings.ApiUri.ToLower().Contains("/prs/") ? HrblEnvironment.PRS :
            (_settings.ApiUri.ToLower().Contains("/prod/") ? HrblEnvironment.Prod : HrblEnvironment.Unknown));

        public HrblOrderingAdapter(HrblOrderingAdapterSettings settings, ILogger<HrblOrderingAdapter> logger = null)
        {
            _logger = logger;
            _settings = settings;
            _proxy = new HLOnlineOrderingRS(new Uri(_settings.ApiUri));
            _logger?.LogInformation($"Proxy instance to {settings.ApiUri} created");
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
        public async Task<SkuInventory[]> GetSkuAvailability(string warehouse, Dictionary<string, int> items)
        {
            if (!items.Any())
                return new SkuInventory[0];

            if (string.IsNullOrWhiteSpace(warehouse))
                throw new ArgumentException("Warehouse must be specified");

            List<SkuInventory> _inventory = new List<SkuInventory>();
            int skip = 0;
            decimal take = 10;

            List<IEnumerable<KeyValuePair<string, int>>> blocks = new List<IEnumerable<KeyValuePair<string, int>>>();
            for (int i = 0; i < (int)Math.Ceiling(items.Count / take); i++)
            {
                blocks.Add(items.Skip(skip).Take((int)take));
                skip += (int)take;
            }

            bool isError = false;
            string error = string.Empty;

            Parallel.ForEach(blocks, b =>
            {
                var request = new
                {
                    ServiceConsumer = _settings.Consumer,
                    SkuInquiryDetails = b.Select(x => new
                    {
                        Sku = new
                        {
                            SkuName = x.Key.ToNormalSku(),
                            Quantity = x.Value.ToString(),
                            WarehouseCode = warehouse
                        }
                    }).ToList()
                };

                _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

                object response = _proxy.GetSkuAvailability.POST(request);

                string data = JsonConvert.SerializeObject(response);
                _logger?.LogInformation($"Result is '{System.Text.Json.JsonSerializer.Serialize(data)}'");

                SkuInventoryDetailsResult result = JsonConvert.DeserializeObject<SkuInventoryDetailsResult>(data);

                if (result.Errors != null && result.Errors.HasErrors)
                {
                    isError = true;
                    error = string.IsNullOrWhiteSpace(result.Errors.ErrorMessage) ? "Unknown error" : result.Errors.ErrorMessage;
                }

                lock (_inventory)
                    _inventory.AddRange(result.SkuInventoryDetails.Inventory);
            });

            if (isError)
                throw new HrblRestApiException(error);

            return _inventory.ToArray();
        }

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="sku">sku to request</param>
        /// <param name="quantity"></param>
        public async Task<SkuInventory> GetSkuAvailability(string warehouse, string sku, int quantity)
            => await GetSkuAvailability(warehouse, new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>(sku, quantity) }
                .ToDictionary(x => x.Key, x => x.Value)).ContinueWith(x => x.Result.FirstOrDefault());

        public async Task<InventoryItem[]> GetProductInventory(string country, string orderType = null)
        {
            OrderType type = string.IsNullOrWhiteSpace(orderType) ? OrderType.Rso
                : EnumHelper.GetValueFromDescription<OrderType>(orderType);

            try
            {
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
            catch (Exception ex)
            {
                return null;
            }
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

            var request = new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
            };

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.GetDistributorProfile.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

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
                DistributorContact contactToUpdate = profile.Shipping?.Contacts?.FirstOrDefault(x => x.Type.Equals(request.Contact.Type, StringComparison.InvariantCultureIgnoreCase)
                    && (x.SubType.Equals(request.Contact.SubType, StringComparison.InvariantCultureIgnoreCase)
                        || (string.IsNullOrEmpty(request.Contact.SubType) && x.IsActive)));

                if (contactToUpdate != null)
                    request.Contact.FillInWithUnspecifiedData(contactToUpdate);
                // else request.Contact = null; // We're not allowed to create new contact
            }

            object response = await _proxy.UpdateDsAddressContacts.POSTAsync(request);
        }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID is mandatory");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is mandatory");

            distributorId = distributorId.ToUpper();

            var request = new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorID = distributorId,
                CountryCode = country.ToUpper()
            };

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.DSFOPPurchasingLimits.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonConvert.DeserializeObject<FOPPurchasingLimitsResult>(JsonConvert.SerializeObject(response)
                , new HrblNullableResponseConverter<FOPPurchasingLimitsResult>());
        }

        public async Task<TinDetails> GetDistributorTins(string distributorId, string country)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID is mandatory");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is mandatory");

            try // Stub: we should get some default tin details
            {
                object response = await _proxy.DistributorTins.POSTAsync(new
                {
                    ServiceConsumer = _settings.Consumer,
                    DistributorId = distributorId,
                    CountryCode = country.ToUpper()
                });

                return JsonConvert.DeserializeObject<GetDistributorTinsResult>(JsonConvert.SerializeObject(response)
                    , new HrblNullableResponseConverter<GetDistributorTinsResult>())?.TinDetails;
            }
            catch
            {
                return new TinDetails
                {
                    DistributorTins = new DistributorTin[] {
                        new DistributorTin {
                            Country = country,
                            Code = country + country,
                            ExpirationDate = DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss+zzz"),
                            EffectiveDate = DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd HH:mm:ss+zzz"),
                            _isActive = "Y"
                        }
                    }
                };
            }
        }

        public async Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID must be specified");

            var request = new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
                FromMonth = month.ToString("yyyy/MM"),
                ToMonth = monthTo.HasValue ? monthTo.Value.ToString("yyyy/MM") : month.ToString("yyyy/MM"),
                IncludeORgVolumes = "N"
            };

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.GetDistributorVolumePoints.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonConvert.DeserializeObject<DistributorVolumePointsDetailsResult>(JsonConvert.SerializeObject(response),
                new HrblNullableResponseConverter<DistributorVolumePointsDetailsResult>()).DistributorVolumeDetails.DistributorVolume;
        }

        public async Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country)
        {
            var request = new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId.ToUpper(),
                OrderMonth = month.ToString("yyyy/MM"),
                ShipToCountry = country
            };

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.GetDistributorDiscount.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

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
            var request = new
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId.ToUpper(),
                ShipToCountry = country,
                PaymentMethod = "CASH"
            };

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.DsCashLimit.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonConvert.DeserializeObject<DsCashLimitResult>(JsonConvert.SerializeObject(response));
        }

        public async Task<PricingResponse> GetPriceDetails(Action<PricingRequestBuilder> setupAction)
            => await GetPriceDetails(setupAction.CreateTargetAndInvoke().AddServiceConsumer(_settings.Consumer).Build());

        public async Task<PricingResponse> GetPriceDetails(PricingRequest request)
        {
            request.ServiceConsumer = _settings.Consumer;

            if (string.Equals(request.Header.CountryCode, "ID")) // A stab: Our assumption is that Oracle has invalid timeshift for ID
            {
                DateTime newOrderTime = request.Header.OrderDate.AddHours(-1);
                DateTime newPriceTime = request.Header.PriceDate.AddHours(-1);
                request.Header.OrderDate = newOrderTime;
                request.Header.PriceDate = newPriceTime;
            }

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.GetPriceDetails.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            PricingResponse result = JsonConvert.DeserializeObject<PricingResponse>(JsonConvert.SerializeObject(response),
                new HrblNullableResponseConverter<PricingResponse>());

            if (!string.IsNullOrWhiteSpace(result.Errors?.ErrorMessage))
                throw new ArgumentException(result.Errors.ErrorMessage);

            return result;
        }

        public async Task<string> HpsPaymentGateway(HpsPaymentPayload payload)
        {
            HpsPaymentRequest request = new HpsPaymentRequestBuilder()
                .AddServiceConsumer(_settings.Consumer)
                .AddPayload(payload).Build();

            HpsPaymentResponse result = JsonConvert.DeserializeObject<HpsPaymentResponse>(JsonConvert.SerializeObject(await _proxy.HPSPaymentGateway.POSTAsync(request)),
              new HrblNullableResponseConverter<HpsPaymentResponse>());

            if (result.Errors.HasErrors)
                throw new HrblRestApiException(result.Errors.ErrorMessage);

            return result.PaymentResponse.ApprovalNum;
        }

        public async Task<SubmitResponse> SubmitOrder(Action<SubmitRequestBuilder> setupAction)
        {
            SubmitRequest request = setupAction.CreateTargetAndInvoke()
                .AddServiceConsumer(_settings.Consumer)
                .Build();

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.SubmitOrder.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonConvert.DeserializeObject<SubmitResponse>(JsonConvert.SerializeObject(response));
        }

        public async Task<SubmitResponse> SubmitOrder(SubmitRequest request)
        {
            request.ServiceConsumer = _settings.Consumer;

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.SubmitOrder.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonConvert.DeserializeObject<SubmitResponse>(JsonConvert.SerializeObject(response));
        }

        public async Task<ConversionRateResponse> GetConversionRate(ConversionRateRequest request)
        {
            object response = await _proxy.GetConversionRate.POSTAsync(request);

            return JsonConvert.DeserializeObject<ConversionRateResponse>(JsonConvert.SerializeObject(response));
        }
        #endregion

        #region Common
        public async Task<bool> GetOrderDualMonthStatus(string country)
        {
            if (string.IsNullOrWhiteSpace(country) || country.Trim().Length != 2)
                throw new ArgumentException("Country is mandatory");

            var request = new
            {
                ShipToCountry = country.Trim().ToUpper()
            };

            _logger?.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));

            object response = await _proxy.GetOrderDualMonthStatus.POSTAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

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

        public async Task<GetDSEligiblePromoSKUResponseDTO> GetDSEligiblePromoSKU(GetDSEligiblePromoSKURequestDTO request)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _proxy.BaseUri.AbsoluteUri + "/GetDSEligiblePromoSKU");

            string json = JsonConvert.SerializeObject(request);

            _logger?.LogInformation(json);

            //construct content to send
            httpRequestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var repsonse = await _proxy.HttpClient.SendAsync(httpRequestMessage);
            string responseStr = await repsonse.Content.ReadAsStringAsync();

            _logger?.LogInformation($"Result is '{responseStr}'");

            return JsonConvert.DeserializeObject<GetDSEligiblePromoSKUResponseDTO>(responseStr,
              new HrblNullableResponseConverter<GetDSEligiblePromoSKUResponseDTO>());
        }

        public override string ToString() => Environment.ToString();

        public async Task<PollResult> PollRequest()
        {
            var result = new List<PollUnitResult>();

            Func<Exception, string> _getFullExceptionDetails = ex => ex.Message + (ex.InnerException == null ? string.Empty : (System.Environment.NewLine + ex.InnerException.Message));
            Func<IEnumerable<ActionLevel>, ActionLevel> _getResultLevel = a =>
            {
                if (a.Count() == 0)
                    return ActionLevel.Info;
                else if (a.Count() == 1)
                    return a.First();
                else
                {
                    if (a.Count(x => x == ActionLevel.Info) == a.Count())
                        return ActionLevel.Info;
                    else if (a.Count(x => x == ActionLevel.Warning) == a.Count())
                        return ActionLevel.Warning;
                    else if (a.Count(x => x == ActionLevel.Error) == a.Count())
                        return ActionLevel.Error;
                    else return ActionLevel.Warning;
                }
            };

            #region Profile
            List<ActionLevel> getProfile_resultLevel = new List<ActionLevel>();
            StringBuilder getProfile_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetProfile)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    DistributorProfile profile = await GetProfile(x);
                    sw.Stop();

                    if (profile == null || profile.Id == null || !profile.Id.Equals(x, StringComparison.InvariantCultureIgnoreCase))
                    {
                        getProfile_resultLevel.Add(ActionLevel.Error);
                        getProfile_protocol.AppendLine($"UID {x}: empty response");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getProfile_resultLevel.Add(ActionLevel.Warning);
                        getProfile_protocol.AppendLine($"UID {x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getProfile_resultLevel.Add(ActionLevel.Info);
                        getProfile_protocol.AppendLine($"UID {x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getProfile_resultLevel.Add(ActionLevel.Error);
                    getProfile_protocol.AppendLine($"Customer UID {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetProfile", Level = _getResultLevel(getProfile_resultLevel), Comment = getProfile_protocol.ToString() });
            #endregion

            #region GetDistributorVolumePoints
            List<ActionLevel> getDistributorVolumePoints_resultLevel = new List<ActionLevel>();
            StringBuilder getDistributorVolumePoints_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetVolumePoints)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    DistributorVolumePoints[] dsVolPoints = await GetVolumePoints(x.distributorId, x.month);
                    sw.Stop();

                    if (!dsVolPoints.Any(y => y != null))
                    {
                        getDistributorVolumePoints_resultLevel.Add(ActionLevel.Error);
                        getDistributorVolumePoints_protocol.AppendLine($"UID {x.distributorId}: empty response");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getDistributorVolumePoints_resultLevel.Add(ActionLevel.Warning);
                        getDistributorVolumePoints_protocol.AppendLine($"UID {x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getDistributorVolumePoints_resultLevel.Add(ActionLevel.Info);
                        getDistributorVolumePoints_protocol.AppendLine($"UID {x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getDistributorVolumePoints_resultLevel.Add(ActionLevel.Error);
                    getDistributorVolumePoints_protocol.AppendLine($"Customer UID {x.distributorId}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetDistributorVolumePoints", Level = _getResultLevel(getDistributorVolumePoints_resultLevel), Comment = getDistributorVolumePoints_protocol.ToString() });
            #endregion

            #region GetDSFOPPurchasingLimits
            List<ActionLevel> getDSFOPPurchasingLimits_resultLevel = new List<ActionLevel>();
            StringBuilder getDSFOPPurchasingLimits_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetDSFOPPurchasingLimits)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    FOPPurchasingLimitsResult fopResult = await GetDSFOPPurchasingLimits(x.distributorId, x.country);
                    sw.Stop();

                    if (fopResult == null || fopResult.FopLimit == null || fopResult.DSPurchasingLimits == null)
                    {
                        getDSFOPPurchasingLimits_resultLevel.Add(ActionLevel.Error);
                        getDSFOPPurchasingLimits_protocol.AppendLine($"UID {x}: empty response");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getDSFOPPurchasingLimits_resultLevel.Add(ActionLevel.Warning);
                        getDSFOPPurchasingLimits_protocol.AppendLine($"UID {x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getDSFOPPurchasingLimits_resultLevel.Add(ActionLevel.Info);
                        getDSFOPPurchasingLimits_protocol.AppendLine($"UID {x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getDSFOPPurchasingLimits_resultLevel.Add(ActionLevel.Error);
                    getDSFOPPurchasingLimits_protocol.AppendLine($"Customer UID {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetDSFOPPurchasingLimits", Level = _getResultLevel(getDSFOPPurchasingLimits_resultLevel), Comment = getDSFOPPurchasingLimits_protocol.ToString() });
            #endregion

            #region GetDsCashLimit
            List<ActionLevel> getCashLimit_resultLevel = new List<ActionLevel>();
            StringBuilder getCashLimit_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetCashLimit)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    DsCashLimitResult cashLimitResult = await GetDsCashLimit(x.distributorId, x.country);
                    sw.Stop();

                    if (cashLimitResult == null || cashLimitResult.LimitAmount < 0m)
                    {
                        getCashLimit_resultLevel.Add(ActionLevel.Error);
                        getCashLimit_protocol.AppendLine($"UID {x}: empty response or invalid cash limit");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getCashLimit_resultLevel.Add(ActionLevel.Warning);
                        getCashLimit_protocol.AppendLine($"UID {x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getCashLimit_resultLevel.Add(ActionLevel.Info);
                        getCashLimit_protocol.AppendLine($"UID {x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getCashLimit_resultLevel.Add(ActionLevel.Error);
                    getCashLimit_protocol.AppendLine($"Customer UID {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetDsCashLimit", Level = _getResultLevel(getCashLimit_resultLevel), Comment = getCashLimit_protocol.ToString() });
            #endregion

            #region GetOrderDualMonthStatus
            List<ActionLevel> getDualMonthStatus_resultLevel = new List<ActionLevel>();
            StringBuilder getDualMonthStatus_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetDualMonth)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    bool dualMonthResult = await GetOrderDualMonthStatus(x);
                    sw.Stop();

                    if (dualMonthResult && DateTime.Now.Day > 4 && DateTime.Now.Day < 30)
                    {
                        getDualMonthStatus_resultLevel.Add(ActionLevel.Error);
                        getDualMonthStatus_protocol.AppendLine($"{x}: seems to be invalid dual month result");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getDualMonthStatus_resultLevel.Add(ActionLevel.Warning);
                        getDualMonthStatus_protocol.AppendLine($"{x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getDualMonthStatus_resultLevel.Add(ActionLevel.Info);
                        getDualMonthStatus_protocol.AppendLine($"{x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getDualMonthStatus_resultLevel.Add(ActionLevel.Error);
                    getDualMonthStatus_protocol.AppendLine($"Country {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetOrderDualMonthStatus", Level = _getResultLevel(getDualMonthStatus_resultLevel), Comment = getDualMonthStatus_protocol.ToString() });
            #endregion

            // The Converter works in the production mode only
#if !DEBUG
            #region GetConversionRate
            List<ActionLevel> getConversationRate_resultLevel = new List<ActionLevel>();
            StringBuilder getConversationRate_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetConversationRate)
            {
                try
                {
                    ConversionRateResponse conversationRate = await GetConversionRate(new ConversionRateRequest { ConversionDate = DateTime.UtcNow.ToString("yyyy-MM-dd "), ExchangeRateType = x.exchangeRateType, FromCurrency = x.fromCurrency, ToCurrency = x.toCurrency });
                    if (conversationRate == null || conversationRate.ConversionRate == null || conversationRate.ConversionRate.Value <= 0)
                    {
                        getConversationRate_resultLevel.Add(ActionLevel.Error);
                        getConversationRate_protocol.AppendLine($"{x.exchangeRateType}: invalid rate");
                    }
                    else getConversationRate_resultLevel.Add(ActionLevel.Info);
                }
                catch (Exception ex)
                {
                    getConversationRate_resultLevel.Add(ActionLevel.Error);
                    getConversationRate_protocol.AppendLine($"Country {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetConversionRate", Level = _getResultLevel(getConversationRate_resultLevel), Comment = getConversationRate_protocol.ToString() });
            #endregion
#endif

            #region TinDetails
            List<ActionLevel> getDsTIN_resultLevel = new List<ActionLevel>();
            StringBuilder getDsTIN_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetDistributorTIN)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    TinDetails tinDetails = await GetDistributorTins(x.distributorId, x.country);
                    sw.Stop();

                    if (tinDetails == null)
                    {
                        getDsTIN_resultLevel.Add(ActionLevel.Error);
                        getDsTIN_protocol.AppendLine($"UID {x}: empty response");
                    }
                    else if (sw.Elapsed.TotalSeconds > 60)
                    {
                        getDsTIN_resultLevel.Add(ActionLevel.Warning);
                        getDsTIN_protocol.AppendLine($"UID {x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getDsTIN_resultLevel.Add(ActionLevel.Info);
                        getDsTIN_protocol.AppendLine($"UID {x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getDsTIN_resultLevel.Add(ActionLevel.Error);
                    getDsTIN_protocol.AppendLine($"Customer UID {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetDistributorTins", Level = _getResultLevel(getDsTIN_resultLevel), Comment = getDsTIN_protocol.ToString() });
            #endregion

            #region Distributor Discount
            List<ActionLevel> getDsDiscount_resultLevel = new List<ActionLevel>();
            StringBuilder getDsDiscount_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetDistributorDiscount)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    DistributorDiscountResult dsDiscount = await GetDistributorDiscount(x.distributorId, x.month, x.country);
                    sw.Stop();
                    if (dsDiscount == null || dsDiscount.Discount == null || dsDiscount.Discount.Discount == null)
                    {
                        getDsDiscount_resultLevel.Add(ActionLevel.Error);
                        getDsDiscount_protocol.AppendLine($"UID {x}: empty response");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getDsDiscount_resultLevel.Add(ActionLevel.Warning);
                        getDsDiscount_protocol.AppendLine($"UID {x}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getDsDiscount_resultLevel.Add(ActionLevel.Info);
                        getDsDiscount_protocol.AppendLine($"UID {x}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }

                }
                catch (Exception ex)
                {
                    getDsDiscount_resultLevel.Add(ActionLevel.Error);
                    getDsDiscount_protocol.AppendLine($"Customer UID {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetDistributorDiscount", Level = _getResultLevel(getDsDiscount_resultLevel), Comment = getDsDiscount_protocol.ToString() });
            #endregion

            #region GetSku
            List<ActionLevel> getsku_resultLevel = new List<ActionLevel>();
            StringBuilder getSku_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetSku)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    SkuInventory sku = await GetSkuAvailability(x.warehouse, x.sku, 1);
                    sw.Stop();
                    if (sku == null || !sku.Sku.Equals(x.sku, StringComparison.InvariantCultureIgnoreCase) || !sku.IsSkuValid)
                    {
                        getsku_resultLevel.Add(ActionLevel.Error);
                        getSku_protocol.AppendLine($"Sku {x.sku}: sku not found or invalid");
                    }
                    else if (sw.Elapsed.TotalSeconds > 10)
                    {
                        getsku_resultLevel.Add(ActionLevel.Warning);
                        getSku_protocol.AppendLine($"Sku {x.sku}: too long response- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getsku_resultLevel.Add(ActionLevel.Info);
                        getSku_protocol.AppendLine($"Sku {x.sku}: downloaded in - {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getsku_resultLevel.Add(ActionLevel.Error);
                    getSku_protocol.AppendLine($"Sku {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetSku", Level = _getResultLevel(getsku_resultLevel), Comment = getSku_protocol.ToString() });
            #endregion

            #region GetProductInventory
            List<ActionLevel> getInventory_resultLevel = new List<ActionLevel>();
            StringBuilder getInventory_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetProductInventory)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    InventoryItem[] skus = await GetProductInventory(x);
                    sw.Stop();
                    if (skus == null || skus.Length == 0)
                    {
                        getInventory_resultLevel.Add(ActionLevel.Error);
                        getInventory_protocol.AppendLine($"Country {x}: unable to download the catalog");
                    }
                    else if (sw.Elapsed.TotalSeconds > 30)
                    {
                        getInventory_resultLevel.Add(ActionLevel.Warning);
                        getInventory_protocol.AppendLine($"Country {x}: the catalog is downloading to slow- {sw.Elapsed.ToString("g")}");
                    }
                    else
                    {
                        getInventory_resultLevel.Add(ActionLevel.Info);
                        getInventory_protocol.AppendLine($"Country {x}: the catalog has been downloaded in {sw.Elapsed.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    getsku_resultLevel.Add(ActionLevel.Error);
                    getInventory_protocol.AppendLine($"Country {x}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetProductInventory", Level = _getResultLevel(getInventory_resultLevel), Comment = getInventory_protocol.ToString() });
            #endregion

            #region GetPriceDetails
            List<ActionLevel> getPricingRequest_resultLevel = new List<ActionLevel>();
            StringBuilder getPricingRequest_protocol = new StringBuilder();

            foreach (var x in _settings.PollSettings.Input_for_GetPricingRequests)
            {
                PricingRequest req = JsonConvert.DeserializeObject<PricingRequest>(Encoding.UTF8.GetString(Convert.FromBase64String(x)));

                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    PricingResponse pricingResponse = await GetPriceDetails(req);
                    sw.Stop();
                    if (pricingResponse == null || pricingResponse.Errors?.HasErrors == true)
                    {
                        getPricingRequest_resultLevel.Add(ActionLevel.Error);
                        if (pricingResponse?.Errors?.HasErrors == true)
                            getPricingRequest_protocol.AppendLine($"{req.Header.ExternalOrderNumber}: {pricingResponse.Errors.ErrorMessage}");
                        else
                            getPricingRequest_protocol.AppendLine($"{req.Header.ExternalOrderNumber}: empty response");
                    }
                    else if (sw.Elapsed.TotalSeconds > 30)
                    {
                        getPricingRequest_resultLevel.Add(ActionLevel.Warning);
                        getPricingRequest_protocol.AppendLine($"{req.Header.ExternalOrderNumber}: too long duration- {sw.Elapsed.TotalSeconds} sec");
                    }
                    else getPricingRequest_resultLevel.Add(ActionLevel.Info);
                }
                catch (Exception ex)
                {
                    getPricingRequest_resultLevel.Add(ActionLevel.Error);
                    getPricingRequest_protocol.AppendLine($"{req.Header.ExternalOrderNumber}: {_getFullExceptionDetails(ex)}");
                }
            }

            result.Add(new PollUnitResult { Action = "GetConversionRate", Level = _getResultLevel(getPricingRequest_resultLevel), Comment = getPricingRequest_protocol.ToString() });
            #endregion

            return new PollResult { Level = _getResultLevel(result.Select(x => x.Level)), Timestamp = DateTimeOffset.Now, Items = result };
        }
    }
}