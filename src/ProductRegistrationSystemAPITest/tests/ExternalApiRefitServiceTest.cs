using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductRegistrationSystemAPI.services.external;
using Refit;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;

namespace ProductRegistrationSystemAPITest.tests
{
    public class FakeHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var json = "{\"userId\":1,\"id\":1,\"title\":\"test\",\"body\":\"body\"}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            };
            return Task.FromResult(response);
        }
    }

    [TestClass]
    public class ExternalApiRefitServiceTest
    {
        private IExternalApiService _service = default!;
        private IServiceScope _scope = default!;

        [TestInitialize]
        public void Initialize()
        {
            var services = new ServiceCollection();
            services.AddRefitClient<IJsonPlaceholderApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new System.Uri("http://localhost"))
                    .AddHttpMessageHandler(() => new FakeHandler())
                    .AddPolicyHandler(GetPolicy());
            services.AddScoped<IExternalApiService, ExternalApiService>();
            var provider = services.BuildServiceProvider();
            _scope = provider.CreateScope();
            _service = _scope.ServiceProvider.GetRequiredService<IExternalApiService>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetPolicy()
        {
            var retry = HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(1);
            var circuit = HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(2, System.TimeSpan.FromSeconds(5));
            var bulk = Policy.BulkheadAsync<HttpResponseMessage>(2000, int.MaxValue);
            return Policy.WrapAsync(bulk, Policy.WrapAsync(retry, circuit));
        }

        [TestMethod]
        public async Task GetPostAsync_Returns_Post()
        {
            var post = await _service.GetPostAsync(1);
            Assert.IsNotNull(post);
            Assert.AreEqual(1, post!.Id);
        }
    }
}
