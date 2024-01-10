using businessLogic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductRegistrationSystemAPI.data.cache;
using ProductRegistrationSystemAPI.data.context;
using ProductRegistrationSystemAPI.data.repositories;
using ProductRegistrationSystemAPI.services;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRegistrationSystemAPITest.tests
{
    [TestClass]
    public class CacheHelperTests
    {
        public required IServiceScope _scope;
        public required IMemoryCache _memoryCache;
        public required ICacheHelper _cacheHelper;

        [TestInitialize]
        public void Initialize()
        {            
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddScoped<ICacheHelper,  CacheHelper>();
            services.AddScoped<IProductCache, ProductCache>();
            var serviceProvider = services.BuildServiceProvider();
            _scope = serviceProvider.CreateScope();
            _cacheHelper = _scope.ServiceProvider.GetRequiredService<ICacheHelper>();
        }

        [TestMethod]
        public void GetDictionary_ReturnsNull_WhenKeyDoesNotExist()
        {
            // Arrange
            const string key = "testKey";

            // Act
            Dictionary<int, string>? result = _cacheHelper.GetDictionary(key);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetDictionary_ReturnsCachedValue_WhenKeyExists()
        {
            // Arrange
            const string key = "testKey";            
            var expectedDictionary = new Dictionary<int, string> { { 1, "value1" } };            
            _cacheHelper.SetDictionary(key, expectedDictionary);

            
            // Act
            Dictionary<int, string>? result = _cacheHelper.GetDictionary(key);

            // Assert
            Assert.AreEqual(expectedDictionary, result);
        }

        [TestMethod]
        public void SetDictionary_SetsValueInCache()
        {
            // Arrange
            const string key = "testKey";
            var valueToCache = new Dictionary<int, string> { { 2, "value2" } };

            // Act
            _cacheHelper.SetDictionary(key, valueToCache);

            // Assert
            Assert.AreEqual(valueToCache, _cacheHelper.GetDictionary(key));
        }
        
    }
}
