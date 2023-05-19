using Filuet.Hrbl.Ordering.Abstractions;
using System.Collections.Concurrent;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public class HrblResponseCacheRepository
    {
        #region SsoProfiles
        public SsoAuthResult? GetSsoProfile(string key)
        {
            if (_ssoProfilesCache.TryGetValue(key, out SsoAuthResult? currentValue))
                return currentValue;

            return null;
        }

        public void PutSsoProfile(string login, string key, SsoAuthResult profile)
        {
            login = login.ToLower();

            _ssoProfilesCache.AddOrUpdate(key, x => profile, (x, oldValue) => profile);

            // The code below removes profile from the cache if stored key is different from the key confirmed.
            // E.g. in case the password has been changed
            if (_profilesLoginToKeyRelation.TryGetValue(login, out string? savedKey) && savedKey != key)
                _ssoProfilesCache.TryRemove(savedKey, out _);

            _profilesLoginToKeyRelation.AddOrUpdate(login, x => key, (x, oldValue) => key);
        }

        public bool RemoveSsoProfile(string key)
            => _ssoProfilesCache.TryRemove(key, out _);
        #endregion

        #region Profiles
        public DistributorProfile? GetProfile(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            if (_profilesCache.TryGetValue(key.Trim().ToUpper(), out DistributorProfile? currentValue))
                return currentValue;

            return null;
        }

        public void PutProfile(string key, DistributorProfile profile)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            _profilesCache.AddOrUpdate(key.Trim().ToUpper(), x => profile, (x, oldValue) => profile);
        }

        public bool RemoveProfile(string key)
            => string.IsNullOrWhiteSpace(key) ? false : _ssoProfilesCache.TryRemove(key.Trim().ToUpper(), out _);
        #endregion

        #region DualMonths
        public bool? GetDualMonthStatus(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            if (_dualMonthCache.TryGetValue(key.Trim().ToLower(), out bool currentValue))
                return currentValue;

            return null;
        }

        public void PutDualMonthStatus(string key, bool isDualMonth)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            _dualMonthCache.AddOrUpdate(key.Trim().ToLower(), x => isDualMonth, (x, oldValue) => isDualMonth);
        }

        public bool RemoveDualMonthStatus(string key)
            => string.IsNullOrWhiteSpace(key) ? false : _dualMonthCache.TryRemove(key.Trim().ToLower(), out _);
        #endregion

        private readonly ConcurrentDictionary<string, SsoAuthResult> _ssoProfilesCache = new ConcurrentDictionary<string, SsoAuthResult>();
        private readonly ConcurrentDictionary<string, DistributorProfile> _profilesCache = new ConcurrentDictionary<string, DistributorProfile>();
        private readonly ConcurrentDictionary<string, bool> _dualMonthCache = new ConcurrentDictionary<string, bool>();
        private readonly ConcurrentDictionary<string, string> _profilesLoginToKeyRelation = new ConcurrentDictionary<string, string>();
    }
}