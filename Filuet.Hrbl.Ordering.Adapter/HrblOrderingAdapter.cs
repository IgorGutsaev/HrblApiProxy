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
using Filuet.Hrbl.Ordering.Abstractions.Profile;

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

        #region Inventory
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
        #endregion



        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        public async Task<DistributorProfile> GetProfile(string distributorId)
        {
            object response = await _proxy.GetDistributorProfile.POSTAsync(new {
                ServiceConsumer = _settings.Consumer,
                DistributorId = distributorId,
            });

            return JsonConvert.DeserializeObject<DistributorProfileResult>(JsonConvert.SerializeObject(response)).Profile;
        }
    }
}
