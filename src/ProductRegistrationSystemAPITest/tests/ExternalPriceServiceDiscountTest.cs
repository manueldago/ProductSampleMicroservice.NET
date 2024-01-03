using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ProductRegistrationSystemAPI.services;
using static ProductRegistrationSystemAPI.services.DiscountExternalservice;

namespace ProductRegistrationSystemAPITest.tests
{
    public class ExternalPriceServiceDiscountTest
    {
        [TestClass]
        public class DiscountExternalserviceTests
        {
            
            public required IDiscountExternalService _discountService;
            public required IServiceScope _scope;

            [TestInitialize]
            public void Initialize()
            {
                var services = new ServiceCollection();
                services.AddHttpClient<DiscountExternalservice>();
                services.AddScoped<IDiscountExternalService, DiscountExternalservice>();
                
                var serviceProvider = services.BuildServiceProvider();
                _scope = serviceProvider.CreateScope();
                _discountService = _scope.ServiceProvider.GetRequiredService<IDiscountExternalService>();
            }

            [TestMethod]
            public async Task GetWithoutDiscount_ReturnsZero_WhenExternalApiReturnsSuccess()
            {                
                // Act
                int discount = await _discountService.GetWithoutDiscount();

                // Assert
                Assert.AreEqual(0, discount);
            }            

            [TestMethod]
            public async Task GetDiscount_ReturnsCorrectDiscount_WhenExternalApiReturnsValidResponse()
            {
                // Arrange
                string discountInfo = JsonConvert.SerializeObject(new DiscountObj[] { new DiscountObj { Value = "1" } });
            
                // Act
                int discount = await _discountService.GetDiscount();

                // Assert
                Assert.IsTrue(discountInfo.Contains(discount.ToString()));
            }
           
        }
    }
}
