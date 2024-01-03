using System.Net.Http;
using System.Text.Json;

namespace ProductRegistrationSystemAPI.services
{

    public class DiscountExternalservice : IDiscountExternalService
    {

        private readonly HttpClient _httpClient;
        private const string MockApiUrl = "https://65931d1ebb12970719905fee.mockapi.io";

        public DiscountExternalservice(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(MockApiUrl);
        }

        public async Task<int> GetWithoutDiscount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/v1/discountzero");
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException(nameof(response));
                }

                var discountInfo = await response.Content.ReadAsStringAsync();
                return int.Parse(discountInfo);

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> GetDiscount()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/v1/discount");
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException(nameof(response));
                }

                var discountInfo = await response.Content.ReadAsStringAsync();
                if (discountInfo == null)
                {
                    throw new ArgumentNullException(nameof(discountInfo));
                }
                
                var discountApplied = JsonSerializer.Deserialize<DiscountObj[]>(discountInfo);
                if (discountApplied == null)
                {
                    throw new ArgumentNullException(nameof(discountApplied));
                }
                
                int discount = int.Parse(discountApplied[0].Value ?? "0");
                return discount;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public class DiscountObj
        {
            public string? Value { get; set; }
        }
    }
   
}