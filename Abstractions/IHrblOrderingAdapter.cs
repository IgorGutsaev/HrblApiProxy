using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public interface IHrblOrderingAdapter
    {
        Task<SkuInventory[]> GetSkuAvailability(string warehouse, Dictionary<string, uint> items);

        /// <summary>
        /// Get remains of goods
        /// </summary>
        /// <param name="warehouse">Warehouse to request</param>
        /// <param name="sku">sku to request</param>
        /// <param name="quantity"></param>
        Task<SkuInventory[]> GetSkuAvailability(string warehouse, string sku, uint quantity);

        /// <summary>
        /// Get distributor (customer) profile
        /// </summary>
        /// <param name="distributorId">Herbalife distributor id</param>
        /// <returns></returns>
        Task<DistributorProfile> GetProfile(string distributorId);

        Task<FOPPurchasingLimitsResult> GetDSFOPPurchasingLimits(string distributorId, string country);

        Task<DistributorVolumePoints[]> GetVolumePoints(string distributorId, DateTime month, DateTime? monthTo = null);

        Task<bool> GetOrderDualMonthStatus(string country);
    }
}