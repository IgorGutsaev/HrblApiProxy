using Filuet.Hrbl.Ordering.Abstractions;
using System.Collections.Concurrent;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public class HrblResponseCacheRepository
    {
        public SsoAuthDistributorDetails? GetSsoProfile(string key)
        {
            if (_profilesCache.TryGetValue(key, out SsoAuthDistributorDetails? currentValue))
                return currentValue;

            return null;
        }

        public void PutSsoProfile(string login, string key, SsoAuthDistributorDetails profile)
        {
            login = login.ToLower();

            _profilesCache.AddOrUpdate(key, x => profile, (x, oldValue) => profile);

            // The code below removes profile from the cache if stored key is different from the key confirmed.
            // E.g. in case the password has been changed
            if (_profilesLoginToKeyRelation.TryGetValue(login, out string? savedKey) && savedKey != key)
                _profilesCache.TryRemove(savedKey, out _);

            _profilesLoginToKeyRelation.AddOrUpdate(login, x => key, (x, oldValue) => key);
        }

        public bool RemoveSsoProfile(string key)
            => _profilesCache.TryRemove(key, out _);

        private readonly ConcurrentDictionary<string, SsoAuthDistributorDetails> _profilesCache = new ConcurrentDictionary<string, SsoAuthDistributorDetails>();
        private readonly ConcurrentDictionary<string, string> _profilesLoginToKeyRelation = new ConcurrentDictionary<string, string>();
    }
}