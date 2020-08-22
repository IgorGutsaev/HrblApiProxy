using Filuet.Fusion.SDK;
using Microsoft.Rest;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Filuet.Hrbl.Ordering.Abstractions.Warehouse;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapter
    {
        /// <summary>
        /// Hrbl auto-generated proxy for REST API
        /// </summary>
        private readonly HLOnlineOrderingRS _proxy;
        private readonly HrblOrderingAdapterSettings _settings;

        public HrblOrderingAdapter(HrblOrderingAdapterSettings settings)
        {
            _settings = settings;
            _proxy = new HLOnlineOrderingRS(new Uri(_settings.ApiUri));
            _proxy.SerializationSettings = null;
            _proxy.DeserializationSettings = null;
            _proxy.HttpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(_settings.Login, _settings.Password);
        }

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="items">collection of goods identifier</param>
        public async Task<SkuInventory[]> GetSkuAvailability(string warehouse, Dictionary<string, uint> items)
        {
            object response = await _proxy.GetSkuAvailability.POSTAsync(new {
                ServiceConsumer = _settings.Consumer,
                SkuInquiryDetails = items.Select(x => new {
                    Sku = new {
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
        public async Task<SkuInventory[]> GetSkuAvailability(string warehouse, string sku, uint quantity)
            => await GetSkuAvailability(warehouse, new List<KeyValuePair<string, uint>> { new KeyValuePair<string, uint>(sku, quantity) }
                                .ToDictionary(x => x.Key, x => x.Value));

        //public async Task<dynamic> Profile(string memberId)
        //{
        //    HttpOperationResponse rest = await _api.GetDistributorProfileWithHttpMessagesAsync(new Filuet.Fusion.SDK.Models.Body15
        //    {
        //        DistributorId = memberId,
        //        ServiceConsumer = Consumer
        //    });

        //    return ((JObject)JsonConvert.DeserializeObject(rest.Response.Content.ReadAsStringAsync().Result));
        //}
    }
}
