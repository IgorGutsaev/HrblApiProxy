using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Infrastructure.Abstractions.Enums;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public interface IHrblOrderingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="force">Since we may need sso valid token (to pay via HPS e.g.) sometimes we need to send straight request</param>
        /// <returns></returns>
        Task<SsoAuthResult> GetSsoProfileAsync(string login, string password, bool force = false);

        Task<DistributorProfile> GetDistributorProfileAsync(string memberId);

        Task<DistributorVolumePoints[]> GetDistributorVolumePointsAsync(string memberId, DateTime month, DateTime? monthTo = null);

        Task<FOPPurchasingLimitsResult> GetDSFOPLimitsAsync(string memberId, Country country);

        Task<TinDetails> GetDistributorTinsAsync(string memberId, Country country);

        Task<bool> GetOrderDualMonthStatusAsync(string country);
    }
}