using Filuet.Infrastructure.DataProvider.Interfaces;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Infrastructure.DataProvider;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public partial class HrblOrderingCaсhingService : IHrblOrderingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter">Adapter to Herbalife rest api</param>
        /// <param name="shortCache">Stores recent responses. We do not have to request Herbalife if the responce in the short cache</param>
        /// <param name="longCache">Long living profile in-memory storage</param>
        /// <param name="logger"></param>
        public HrblOrderingCaсhingService(IHrblOrderingAdapter adapter,
            IMemoryCachingService shortCache,
            HrblResponseCacheRepository longCache,
            ILogger<IHrblOrderingService> logger)
        {
            _adapter = adapter;
            _longCache = longCache;
            _shortCache = shortCache;
            _logger = logger;
        }

        public async Task<SsoAuthResult> GetSsoProfileAsync(string login, string password, bool force = false)
        {
            string key = $"{login.ToLower()}+{password}";
            SsoAuthResult? cachedProfile = _longCache.GetSsoProfile(key);

            MemoryCacher memCacher = _shortCache.Get(SSO_PROFILE_CACHE_NAME, SSO_PROFILE_CACHE_SIZE_MB);

            Func<Task<SsoAuthResult>> extractProfile = async () =>
            {
                // cached profile has not been found. We must request Herbalife
                SsoAuthResult authResponse;
                try
                {
                    authResponse = await _adapter.GetSsoProfileAsync(login, password);

                    if (authResponse != null)
                    {
                        // put response to the short cache
                        memCacher.Set(key, authResponse, SSO_PROFILE_CACHE_DURATION_MIN * 60000, false);
                        // put response to the long cache
                        _longCache.PutSsoProfile(login, key, authResponse);

                        return authResponse;
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    // clean the caches if authentication failed
                    memCacher.Cache.Remove(key);
                    _longCache.RemoveSsoProfile(key);
                    throw ex;
                }

                throw new UnauthorizedAccessException();
            };

            if (cachedProfile != null && !force)
            {
                // the profile has been found in the long cache.
                // but password may have changed so let's request to Herbalife for verification 

                // first let's check 'short' cache. The profile in the short cache means that login/password provided were up to date as of cache lifetime ago
                SsoAuthResult fromCache = memCacher.Get<SsoAuthResult>(key);
                if (fromCache != null)
                    return fromCache;
                else
                {
                    // if there'is no profile in the short cache we can return the found profile from long cache
                    // but with the condition that we will validate profile shortly and if the key has changed:
                    _ = Task.Run(extractProfile).ConfigureAwait(false);

                    return cachedProfile;
                }
            }
            else
                return await extractProfile(); // get the profile from Herbalife and attach it to the caches
        }

        public async Task<DistributorProfile> GetDistributorProfileAsync(string memberId)
        {
            DistributorProfile? cachedProfile = _longCache.GetProfile(memberId);

            MemoryCacher memCacher = _shortCache.Get(PROFILE_CACHE_NAME, PROFILE_CACHE_SIZE_MB);

            Func<Task<DistributorProfile>> extractProfile = async () =>
            {
                // cached profile has not been found. We must request Herbalife
                DistributorProfile profile;
                try
                {
                    profile = await _adapter.GetProfileAsync(memberId);

                    if (profile != null)
                    {
                        // put response to the short cache
                        memCacher.Set(memberId, profile, PROFILE_CACHE_DURATION_MIN * 60000, false);
                        // put response to the long cache
                        _longCache.PutProfile(memberId, profile);

                        return profile;
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    // clean the caches if authentication failed
                    memCacher.Cache.Remove(memberId);
                    _longCache.RemoveProfile(memberId);
                    throw ex;
                }

                throw new UnauthorizedAccessException();
            };

            if (cachedProfile != null)
            {
                // the profile has been found in the long cache.
                // first let's check 'short' cache
                DistributorProfile fromCache = memCacher.Get<DistributorProfile>(memberId);
                if (fromCache != null)
                    return fromCache;
                else
                {
                    // if there'is no profile in the short cache we can return the found profile from long cache
                    // but with the condition that we will validate profile shortly and if the key has changed:
                    _ = Task.Run(extractProfile).ConfigureAwait(false);

                    return cachedProfile;
                }
            }
            else return await extractProfile(); // get the profile from Herbalife and attach it to the caches
        }

        public async Task<DistributorVolumePoints[]> GetDistributorVolumePointsAsync(string memberId, DateTime month, DateTime? monthTo = null)
        {
            // Volume points can only be stored in the short cache
            MemoryCacher memCacher = _shortCache.Get(VOLUME_POINTS_CACHE_NAME, VOLUME_POINTS_CACHE_SIZE_MB);

            if (monthTo.HasValue && monthTo.Value.Year == month.Year && monthTo.Value.Month == month.Month)
                monthTo = null;

            string key = $"{memberId}_{new DateTime(month.Year, month.Month, 1):d}{(monthTo.HasValue ? new DateTime(monthTo.Value.Year, monthTo.Value.Month, 1).ToString("d") : string.Empty)}";

            DistributorVolumePoints[] result = memCacher.Get<DistributorVolumePoints[]>(key);

            if (result != null)
                return result;

            try
            {
                result = await _adapter.GetVolumePoints(memberId, month, monthTo);

                if (result != null)
                    memCacher.Set(key, result, VOLUME_POINTS_CACHE_DURATION_MIN * 60000, false);
            }
            catch (UnauthorizedAccessException ex)
            {
                memCacher.Cache.Remove(key);
                throw ex;
            }

            return result;
        }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPLimitsAsync(string memberId, Country country)
        {
            // First Order Purchase limits can only be stored in the short cache
            MemoryCacher memCacher = _shortCache.Get(FOP_CACHE_NAME, FOP_CACHE_SIZE_MB);

            string key = $"{memberId}_{country.GetCode()}";

            FOPPurchasingLimitsResult result = memCacher.Get<FOPPurchasingLimitsResult>(key);

            if (result != null)
                return result;

            try
            {
                result = await _adapter.GetDSFOPPurchasingLimits(memberId, country.GetCode());

                if (result != null)
                    memCacher.Set(key, result, FOP_CACHE_DURATION_MIN * 60000, false);
            }
            catch (UnauthorizedAccessException ex)
            {
                memCacher.Cache.Remove(key);
                throw ex;
            }

            return result;
        }

        public async Task<TinDetails> GetDistributorTinsAsync(string memberId, Country country)
        {
            // First Order Purchase limits can only be stored in the short cache
            MemoryCacher memCacher = _shortCache.Get(TIN_CACHE_NAME, TIN_CACHE_SIZE_MB);

            string key = $"{memberId}_{country.GetCode()}";

            TinDetails result = memCacher.Get<TinDetails>(key);

            if (result != null)
                return result;

            try
            {
                result = await _adapter.GetDistributorTins(memberId, country.GetCode());

                if (result != null)
                    memCacher.Set(key, result, TIN_CACHE_DURATION_MIN * 60000, false);
            }
            catch (UnauthorizedAccessException ex)
            {
                memCacher.Cache.Remove(key);
                throw ex;
            }

            return result;
        }

        public async Task<bool> GetOrderDualMonthStatusAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country code is mandatory");

            country = country.ToLower();
            // potential period to apply dual month logic.
            bool dualMonthPotentialPeriod = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) == DateTime.Now.Day || DateTime.Now.Day <= 5;

            if (dualMonthPotentialPeriod) // do not read from cache if there is a possibility that now is a dual month period
                return await _adapter.GetOrderDualMonthStatusAsync(country);

            bool? cachedDualMonths = _longCache.GetDualMonthStatus(country);

            MemoryCacher memCacher = _shortCache.Get(DUAL_MONTH_CACHE_NAME, DUAL_MONTH_CACHE_SIZE_MB);

            Func<Task<bool>> extractDualMonth = async () =>
            {
                // cached dual month hasn't been found. We must request Herbalife
                bool isDualMonth;
                try
                {
                    isDualMonth = await _adapter.GetOrderDualMonthStatusAsync(country);

                    // put response to the short cache
                    memCacher.Set(country.ToLower(), isDualMonth, DUAL_MONTH_CACHE_DURATION_MIN * 60000, false);
                    // put response to the long cache
                    _longCache.PutDualMonthStatus(country, isDualMonth);

                    return isDualMonth;
                }
                catch (Exception ex)
                {
                    // clean both caches if request failed
                    memCacher.Cache.Remove(country);
                    _longCache.RemoveDualMonthStatus(country);
                    throw ex;
                }

                throw new Exception();
            };

            if (cachedDualMonths.HasValue)
            {
                // The profile has been found in the long cache
                // First let's check 'short' cache
                bool? fromCache = memCacher.Get<bool?>(country.ToLower());
                if (fromCache.HasValue)
                    return fromCache.Value;
                else
                {
                    // If there'is no profile in the short cache we can return the found profile from long cache
                    // but with the condition that we will validate profile shortly and if the key has changed:
                    _ = Task.Run(extractDualMonth).ConfigureAwait(false);

                    return cachedDualMonths.Value;
                }
            }
            else
                return await extractDualMonth(); // get the profile from Herbalife and attach it to the caches
        }

        private readonly IHrblOrderingAdapter _adapter;
        private readonly HrblResponseCacheRepository _longCache;
        private readonly IMemoryCachingService _shortCache;
        private readonly ILogger<IHrblOrderingService> _logger;

        const string SSO_PROFILE_CACHE_NAME = "ssoProfiles";
        const int SSO_PROFILE_CACHE_SIZE_MB = 200;
        const int SSO_PROFILE_CACHE_DURATION_MIN = 180;

        const string PROFILE_CACHE_NAME = "profiles";
        const int PROFILE_CACHE_SIZE_MB = 200;
        const int PROFILE_CACHE_DURATION_MIN = 180;

        const string VOLUME_POINTS_CACHE_NAME = "volumePoints";
        const int VOLUME_POINTS_CACHE_SIZE_MB = 10;
        const int VOLUME_POINTS_CACHE_DURATION_MIN = 2;

        const string FOP_CACHE_NAME = "dsFopLimits";
        const int FOP_CACHE_SIZE_MB = 10;
        const int FOP_CACHE_DURATION_MIN = 2;

        const string TIN_CACHE_NAME = "tins";
        const int TIN_CACHE_SIZE_MB = 10;
        const int TIN_CACHE_DURATION_MIN = 180;

        const string DUAL_MONTH_CACHE_NAME = "dualMonthStatuses";
        const int DUAL_MONTH_CACHE_SIZE_MB = 1;
        const int DUAL_MONTH_CACHE_DURATION_MIN = 180;
    }
}