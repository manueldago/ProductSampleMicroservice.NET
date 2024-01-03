using models;
using System.Drawing;

namespace ProductRegistrationSystemAPI.data.cache
{
    public class ProductCache : IProductCache
    {
        public string key => "productCache";

        private readonly ICacheHelper _cacheHelper;

        private Dictionary<int, string> StatusNameDictionary => 
        new Dictionary<int, string>
        {
            { 1, "Active" },
            { 0, "Inactive" }
        };

        
        public ProductCache(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;            
        }

        public string GetStatusName(byte status)
        {            
            var statusFromCache = _cacheHelper.GetDictionary(key);
            
            if (statusFromCache == null) 
            {              
                _cacheHelper.SetDictionary(key, StatusNameDictionary);                
                
                return StatusNameDictionary[status];
            }

            var result = statusFromCache[status];

            return result;
        }
    }
}
