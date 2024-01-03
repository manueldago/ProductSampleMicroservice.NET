using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace ProductRegistrationSystemAPI.data.cache
{
    public class CacheHelper : ICacheHelper 
    {
        public readonly IMemoryCache _memoryCache;
        public int _timeCache { get; set; }
        public CacheHelper(IMemoryCache memoryCache, int timeCache = 5)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _timeCache = timeCache;
        }
        public void SetMinutesFromCache(int time)
        {
            _timeCache = time;
        }

        public Dictionary<int,string>? GetDictionary(string key)
        {
            return _memoryCache.TryGetValue(key, out Dictionary<int,string>? valueFromCache) ? valueFromCache : null;
        }

        public void SetDictionary(string key, Dictionary<int,string> value)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_timeCache)
            });
        }
    }
}

