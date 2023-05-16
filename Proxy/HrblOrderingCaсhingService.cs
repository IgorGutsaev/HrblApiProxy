using Filuet.Infrastructure.DataProvider.Interfaces;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Infrastructure.DataProvider;

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
                // Cached profile has not been found. We must request Herbalife
                SsoAuthResult authResponse;
                try
                {
                    authResponse = await _adapter.GetSsoProfileAsync(login, password);

                    if (authResponse != null)
                    {
                        // Put response to the short cache
                        memCacher.Set(key, authResponse, SSO_PROFILE_CACHE_DURATION_MIN * 60000, false);
                        // Put response to the long cache
                        _longCache.PutSsoProfile(login, key, authResponse);
                        // + add hrbl token

                        return authResponse;
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Clean the caches if authentication failed
                    memCacher.Cache.Remove(key);
                    _longCache.RemoveSsoProfile(key);
                    throw ex;
                }

                throw new UnauthorizedAccessException();
            };

            if (cachedProfile != null && !force)
            {
                // The profile has been found in the long cache. But password may have changed
                // so let's request to Herbalife for verification 

                // First let's check 'short' cache. The profile in the short cache means that login/password provided were up to date as of cache lifetime ago
                SsoAuthResult fromCache = memCacher.Get<SsoAuthResult>(key);
                if (fromCache != null)
                    return fromCache;
                else
                {
                    // If there'is no profile in the short cache we can return the found profile from long cache
                    // but with the condition that we will validate profile shortly and if the key has changed:
                    _ = Task.Run(extractProfile).ConfigureAwait(false);

                    return cachedProfile;
                }
            }
            else
                return await extractProfile(); // get the profile from Herbalife and attach it to the caches
        }

        private readonly IHrblOrderingAdapter _adapter;
        private readonly HrblResponseCacheRepository _longCache;
        private readonly IMemoryCachingService _shortCache;
        private readonly ILogger<IHrblOrderingService> _logger;

        const string SSO_PROFILE_CACHE_NAME = "ssoProfiles";
        const int SSO_PROFILE_CACHE_SIZE_MB = 100; 
        const int SSO_PROFILE_CACHE_DURATION_MIN = 180; 
    }
}