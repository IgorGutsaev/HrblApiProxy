using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
using Filuet.Hrbl.Ordering.SDK;
using System.Text.Json;
using System.Buffers;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Net;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapter : IHrblOrderingAdapter
    {
        private const string hlbuild = "1.0.1";
        private const string hlappid = "1";
        private const string hlclientdevice = "HUAWEI HUAWEI MT7-TL10 Huawei/MT7-TL10/hwmt7:4.4.2/ HuaweiMT7 - TL10 / C00B130:user/ota-rel-keys,release-keys";
        private const string hlclientapp = "SHOP";
        private const string hllocale = "en-US";
        private const string hlclientos = "android/4.4.2:C00B130";

        public HrblEnvironment Environment => _settings.ApiUri.ToLower().Contains("/ts3/") ? HrblEnvironment.TS3 :
            (_settings.ApiUri.ToLower().Contains("/prs/") ? HrblEnvironment.PRS :
            (_settings.ApiUri.ToLower().Contains("/prod/") ? HrblEnvironment.Prod : HrblEnvironment.Unknown));

        public HrblOrderingAdapter(HrblOrderingAdapterSettings settings, ILogger<HrblOrderingAdapter> logger = null)
        {
            _logger = logger;
            _settings = settings;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.Login}:{_settings.Password}")));

            _proxy = new HrblRestApiClient(client);
            _proxy.BaseUrl = settings.ApiUri;

            _logger?.LogInformation($"Proxy instance to {settings.ApiUri} created");
        }

        #region Inventory
        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="items">collection of goods identifier</param>
        public async Task<SkuInventory[]> GetSkuAvailabilityAsync(string warehouse, Dictionary<string, int> items)
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

            List<Task<string>> chunkTasks = new List<Task<string>>();

            foreach (var b in blocks)
            {
                var request = new GetSkuAvailability_body
                {
                    ServiceConsumer = _settings.Consumer,
                    SkuInquiryDetails = b.Select(x => new OrderHLOnlineOrderingts3GetSkuAvailability_SkuInquiryDetails
                    {
                        Sku = new OrderHLOnlineOrderingts3GetSkuAvailability_Sku
                        {
                            SkuName = x.Key.ToNormalSku(),
                            Quantity = x.Value.ToString(),
                            WarehouseCode = warehouse
                        }
                    }).ToList()
                };

                _logger?.LogInformation(JsonSerializer.Serialize(request));

                chunkTasks.Add(_proxy.GetSkuAvailabilityAsync(request));
            }

            object[] responses = await Task.WhenAll(chunkTasks);

            foreach (object response in responses)
            {
                string data = JsonSerializer.Serialize(response);
                _logger?.LogInformation($"Result is '{data}'");

                SkuInventoryDetailsResult result = JsonSerializer.Deserialize<SkuInventoryDetailsResult>(data);

                if (result.Errors != null && result.Errors.HasErrors)
                {
                    isError = true;
                    error = string.IsNullOrWhiteSpace(result.Errors.ErrorMessage) ? "Unknown error" : result.Errors.ErrorMessage;
                }

                lock (_inventory)
                    _inventory.AddRange(result.SkuInventoryDetails.Inventory);
            }

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
        public async Task<SkuInventory> GetSkuAvailabilityAsync(string warehouse, string sku, int quantity)
            => await GetSkuAvailabilityAsync(warehouse, new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>(sku, quantity) }
                .ToDictionary(x => x.Key, x => x.Value)).ContinueWith(x => x.Result.FirstOrDefault());

        public async Task<InventoryItem[]> GetProductInventory(string country, string orderType = null)
        {
            OrderType type = string.IsNullOrWhiteSpace(orderType) ? OrderType.Rso
                : EnumHelper.GetValueFromDescription<OrderType>(orderType);

            try
            {
                string response = await _proxy.GetProductInventoryAsync(new GetProductInventory_body
                {
                    CountryCode = country,
                    OrderType = EnumHelper.GetDescription(type)
                });

                InventoryResult result = JsonSerializer.Deserialize<InventoryResult>(response.ResolveHrblMess());
                if (!result.Inventory.IsSussess)
                    throw new HrblRestApiException($"An error occured while requesting product inventory in {country}");

                return result.Inventory.Inventories.ItemsRoot.Items;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CatalogItem[]> GetProductCatalog(string country, string orderType = null)
        {
            OrderType type = string.IsNullOrWhiteSpace(orderType) ? OrderType.Rso
                : EnumHelper.GetValueFromDescription<OrderType>(orderType);

            string response = await _proxy.GetProductCatalogAsync(new GetProductCatalog_body
            {
                CountryCode = country,
                OrderType = EnumHelper.GetDescription(type)
            });

            return JsonSerializer.Deserialize<CatalogResult>(response.ResolveHrblMess()).CatalogDetails.Items;
        }
        #endregion

        #region Distributor
        public async Task<SsoAuthResult> GetSsoProfileAsync(string login, string password)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_settings.SSOAuthServiceUri);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/auth");
                request.Headers.Add("X-HLBUILD", hlbuild);
                request.Headers.Add("X-HLAPPID", hlappid);
                request.Headers.Add("X-HLCLIENTDEVICE", hlclientdevice);
                request.Headers.Add("X-HLCLIENTAPP", hlclientapp);
                request.Headers.Add("X-HLLOCALE", hllocale);
                request.Headers.Add("X-HLCLIENTOS", hlclientos);
                request.Content = new StringContent($"{{ \"data\": {{ \"locale\": \"{hllocale}\", \"username\": \"{login}\", \"password\": \"{password}\" }}}}"
                    , Encoding.Default, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.Forbidden)
                        throw new UnauthorizedAccessException("Not authenticated");

                    throw ex;
                }

                string resultStr = response.Content.ReadAsStringAsync().Result;
                SsoAuthResposeWrapper result = JsonSerializer.Deserialize<SsoAuthResposeWrapper>(resultStr);
                if (result.Data == null || !result.Data.IsAuthenticated)
                    throw new UnauthorizedAccessException(result.Message);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Data.Token);

                HttpResponseMessage messageDetails = httpClient.GetAsync("/api/Distributor/?type=Detailed").Result;
                resultStr = messageDetails.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                SsoAuthDistributorDetails details = JsonSerializer.Deserialize<SsoAuthDistributorDetails>(resultStr);

                return new SsoAuthResult { Token = result.Data.Token, Profile = details };
            }
        }

        public async Task<(bool isValid, string memberId)> ValidateSsoBearerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Country is mandatory");

            string response = await _proxy.ValidateAsync(new Api_validate_body { AccessToken = token, XHLAPPID = _settings.OrganizationId.ToString() });

            ApiValidateResponse result = JsonSerializer.Deserialize<ApiValidateResponse>(response.ResolveHrblMess());

            return (result.IsValid, result.MemberId);
        }

        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        public async Task<DistributorProfile> GetProfileAsync(string distributorId)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID must be specified");

            var request = new GetDistributorProfile_body
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
            };

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            object response = await _proxy.GetDistributorProfileAsync(request);

            string responseString = JsonSerializer.Serialize(response);

            _logger?.LogInformation($"Result is '{responseString}'");

            return JsonSerializer.Deserialize<DistributorProfileResult>(responseString.ResolveHrblMess()).Profile;
        }

        public async Task UpdateAddressAndContacts(Action<ProfileUpdateBuilder> setup)
        {
            UpdateAddressAndContactsRequest request =
                setup.CreateTargetAndInvoke().SetServiceConsumer(_settings.Consumer).Build();

            DistributorProfile profile = await GetProfileAsync(request.DistributorId);

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

            object response = await _proxy.UpdateDsAddressContactsAsync(JsonSerializer.Deserialize<UpdateDsAddressContacts_body>(JsonSerializer.Serialize(request)));
        }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID is mandatory");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is mandatory");

            distributorId = distributorId.ToUpper();

            var request = new DSFOPPurchasingLimits_body
            {
                ServiceConsumer = _settings.Consumer,
                DistributorID = distributorId,
                CountryCode = country.ToUpper()
            };

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            object response = await _proxy.DSFOPPurchasingLimitsAsync(request);

            string responseString = JsonSerializer.Serialize(response);

            _logger?.LogInformation($"Result is '{responseString}'");

            return JsonSerializer.Deserialize<FOPPurchasingLimitsResult>(responseString.ResolveHrblMess());
        }

        public async Task<TinDetails> GetDistributorTins(string distributorId, string country)
        {
            if (string.IsNullOrWhiteSpace(distributorId))
                throw new ArgumentException("Distributor ID is mandatory");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is mandatory");

            try // Stub: we should get some default tin details
            {
                object response = await _proxy.GetDistributorTinsAsync(new GetDistributorTins_body
                {
                    ServiceConsumer = _settings.Consumer,
                    DistributorId = distributorId,
                    CountryCode = country.ToUpper()
                });

                string responseString = JsonSerializer.Serialize(response);

                _logger?.LogInformation($"Result is '{responseString}'");

                return JsonSerializer.Deserialize<GetDistributorTinsResult>(responseString.ResolveHrblMess())?.TinDetails;
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

            var request = new GetDistributorVolumePoints_body
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
                FromMonth = month.ToString("yyyy/MM"),
                ToMonth = monthTo.HasValue ? monthTo.Value.ToString("yyyy/MM") : month.ToString("yyyy/MM"),
                IncludeORgVolumes = "N"
            };

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            object response = await _proxy.GetDistributorVolumePointsAsync(request);

            _logger?.LogInformation($"Result is '{JsonSerializer.Serialize((JsonElement)response)}'");

            return JsonSerializer.Deserialize<DistributorVolumePointsDetailsResult>((JsonElement)response).DistributorVolumeDetails.DistributorVolume;
        }

        public async Task<DistributorDiscountResult> GetDistributorDiscount(string distributorId, DateTime month, string country)
        {
            var request = new GetDistributorDiscount_body
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId.ToUpper(),
                OrderMonth = month.ToString("yyyy/MM"),
                ShipToCountry = country
            };

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            string response = await _proxy.GetDistributorDiscountAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonSerializer.Deserialize<DistributorDiscountResult>(response.ResolveHrblMess());
        }

        /// <summary>
        /// Get member cash limit
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country">Shipp to country</param>
        /// <returns></returns>
        public async Task<DsCashLimitResult> GetDsCashLimit(string distributorId, string country)
        {
            var request = new DsCashLimit_body
            {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId.ToUpper(),
                ShipToCountry = country,
                PaymentMethod = "CASH"
            };

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            string response = await _proxy.DsCashLimitAsync(request);

            _logger?.LogInformation($"Result is '{response}'");

            return JsonSerializer.Deserialize<DsCashLimitResult>(response.ResolveHrblMess());
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

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            GetPriceDetails_body body = new GetPriceDetails_body
            {
                ServiceConsumer = request.ServiceConsumer,
                OrderPriceHeader = new OrderHLOnlineOrderingts3GetPriceDetails_OrderPriceHeader
                {
                    OrderSource = request.Header.OrderSource,
                    ExternalOrderNumber = request.Header.ExternalOrderNumber,
                    DistributorId = request.Header.DistributorId,
                    Warehouse = request.Header.Warehouse,
                    ProcessingLocation = request.Header.ProcessingLocation,
                    FreightCode = request.Header.FreightCode,
                    CountryCode = request.Header.CountryCode,
                    OrderMonth = request.Header.OrderMonth.ToString("yyMM"),
                    OrderCategory = request.Header.OrderCategory,
                    OrderType = request.Header.OrderType,
                    PriceDate = request.Header.PriceDate.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    OrderDate = request.Header.OrderDate.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    CurrencyCode = request.Header.CountryCode,
                    OrderSubType = request.Header.OrderSubType,
                    PostalCode = request.Header.PostalCode,
                    City = request.Header.City,
                    State = request.Header.State,
                    Province = request.Header.Province,
                    County = request.Header.County,
                    Address1 = request.Header.Address1,
                    Address2 = request.Header.Address2,
                    Address3 = request.Header.Address3,
                    Address4 = request.Header.Address4,
                    ShipFromOrgId = request.Header.OrgID.ToString(),
                    OrderTypeId = request.Header.OrderTypeID?.ToString() ?? string.Empty
                },
                OrderPriceLines = request.Lines.Select(x => new OrderHLOnlineOrderingts3GetPriceDetails_OrderPriceLines
                {
                    SellingSKU = x.Sku,
                    OrderedQty = x.Quantity.ToString(),
                    ProcessingLocation = x.ProcessingLocation,
                    ProductType = x.ProductType
                }).ToList()
            };

            object response = await _proxy.GetPriceDetailsAsync(body);
            string responseString = JsonSerializer.Serialize(response);
            _logger?.LogInformation($"Result is '{responseString}'");

            PricingResponse result = JsonSerializer.Deserialize<PricingResponse>(responseString.ResolveHrblMess());

            if (!string.IsNullOrWhiteSpace(result.Errors?.ErrorMessage))
                throw new ArgumentException(result.Errors.ErrorMessage);

            return result;
        }

        public async Task<string> HpsPaymentGateway(HpsPaymentPayload payload)
        {
            HpsPaymentRequest request = new HpsPaymentRequestBuilder()
                .AddServiceConsumer(_settings.Consumer)
                .AddPayload(payload).Build();

            string response = await _proxy.HPSPaymentGatewayAsync(JsonSerializer.Deserialize<HPSPaymentGateway_body>(JsonSerializer.Serialize(request)));

            HpsPaymentResponse result = JsonSerializer.Deserialize<HpsPaymentResponse>(response.ResolveHrblMess());

            if (result.Errors.HasErrors)
                throw new HrblRestApiException(result.Errors.ErrorMessage);

            return result.PaymentResponse.ApprovalNum;
        }

        public async Task<SubmitResponse> SubmitOrder(Action<SubmitRequestBuilder> setupAction)
        {
            SubmitRequest request = setupAction.CreateTargetAndInvoke()
                .AddServiceConsumer(_settings.Consumer)
                .Build();

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            string response = await _proxy.SubmitOrderAsync(JsonSerializer.Deserialize<SubmitOrder_body>(JsonSerializer.Serialize(request)));

            _logger?.LogInformation($"Result is '{response}'");

            return JsonSerializer.Deserialize<SubmitResponse>(response.ResolveHrblMess());
        }

        public async Task<SubmitResponse> SubmitOrder(SubmitRequest request)
        {
            request.ServiceConsumer = _settings.Consumer;

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            string response = await _proxy.SubmitOrderAsync(JsonSerializer.Deserialize<SubmitOrder_body>(JsonSerializer.Serialize(request)));

            _logger?.LogInformation($"Result is '{response}'");

            return JsonSerializer.Deserialize<SubmitResponse>(response.ResolveHrblMess());
        }

        public async Task<ConversionRateResponse> GetConversionRate(ConversionRateRequest request)
        {
            string response = await _proxy.GetConversionRateAsync(JsonSerializer.Deserialize<GetConversionRate_body>(JsonSerializer.Serialize(request)));

            return JsonSerializer.Deserialize<ConversionRateResponse>(response.ResolveHrblMess());
        }
        #endregion

        #region Common
        public async Task<bool> GetOrderDualMonthStatusAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country) || country.Trim().Length != 2)
                throw new ArgumentException("Country is mandatory");

            var request = new GetOrderDualMonthStatus_body
            {
                ShipToCountry = country.Trim().ToUpper()
            };

            _logger?.LogInformation(JsonSerializer.Serialize(request));

            object response = await _proxy.GetOrderDualMonthStatusAsync(request);

            string responseString = JsonSerializer.Serialize(response);

            _logger?.LogInformation($"Result is '{responseString}'");

            return JsonSerializer.Deserialize<OrderDualMonthStatus>(responseString.ResolveHrblMess()).IsDualMonthAllowed;
        }

        public async Task<DsPostamatDetails[]> GetPostamats(string country, string postamatType, string region = null, string city = null, string zipCode = null)
        {
            if (string.IsNullOrWhiteSpace(country) || country.Trim().Length != 2)
                throw new ArgumentException("Country is mandatory");

            if (string.IsNullOrWhiteSpace(postamatType))
                throw new ArgumentException("Postamat is mandatory");

            string response = await _proxy.GetDSPostamatDetailsAsync(new GetDSPostamatDetails_body
            {
                Country = country.Trim(),
                PostamatType = postamatType.Trim(),
                City = city?.Trim(),
                Region = region?.Trim(),
                ZipCode = zipCode?.Trim()
            });

            DSPostamatDetailsResult result = JsonSerializer.Deserialize<DSPostamatDetailsResult>(response.ResolveHrblMess());

            if (result.Errors.HasErrors)
                throw new HrblRestApiException(result.Errors.ErrorMessage);

            return result.DsPostamatDetails;
        }

        public async Task<WHFreightCode[]> GetShippingWhseAndFreightCodes(string postalCode, bool expressDeliveryFlag = true)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code is mandatory");

            string response = await _proxy.GetShippingWhseAndFreightCodesAsync(new GetShippingWhseAndFreightCodes_body
            {
                ServiceConsumer = _settings.Consumer,
                ExpressDeliveryFlag = expressDeliveryFlag ? "Y" : "N",
                PostalCode = postalCode.Trim()
            });

            WHFreightCodesResult result = JsonSerializer.Deserialize<WHFreightCodesResult>(response.ResolveHrblMess());

            if (result.ErrorCode != "0")
                throw new HrblRestApiException(result.ErrorMessage);

            return result.WHFriehtCodes;
        }
        #endregion

        public async Task<GetDSEligiblePromoSKUResponseDTO> GetDSEligiblePromoSKU(GetDSEligiblePromoSKURequestDTO request)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(new Uri(_proxy.BaseUrl), "GetDSEligiblePromoSKU"));

            string json = JsonSerializer.Serialize(request);

            _logger?.LogInformation(json);

            //construct content to send
            httpRequestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var repsonse = await _proxy._httpClient.SendAsync(httpRequestMessage); // make _httpClient public or include GetDSEligiblePromoSKURequestDTO into spec schema ;)
            string responseStr = await repsonse.Content.ReadAsStringAsync();

            _logger?.LogInformation($"Result is '{responseStr}'");

            return JsonSerializer.Deserialize<GetDSEligiblePromoSKUResponseDTO>(responseStr.ResolveHrblMess());
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
                    DistributorProfile profile = await GetProfileAsync(x);
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

            result.Add(new PollUnitResult { Action = "GetProfileAsync", Level = _getResultLevel(getProfile_resultLevel), Comment = getProfile_protocol.ToString() });
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
                    bool dualMonthResult = await GetOrderDualMonthStatusAsync(x);
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

            result.Add(new PollUnitResult { Action = "GetOrderDualMonthStatusAsync", Level = _getResultLevel(getDualMonthStatus_resultLevel), Comment = getDualMonthStatus_protocol.ToString() });
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
                    SkuInventory sku = await GetSkuAvailabilityAsync(x.warehouse, x.sku, 1);
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
                PricingRequest req = JsonSerializer.Deserialize<PricingRequest>(Encoding.UTF8.GetString(Convert.FromBase64String(x)));

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

        /// <summary>
        /// Hrbl auto-generated proxy for REST API
        /// </summary>
        private readonly HrblRestApiClient _proxy;
        private readonly HrblOrderingAdapterSettings _settings;
        private readonly ILogger<HrblOrderingAdapter> _logger;
    }
}