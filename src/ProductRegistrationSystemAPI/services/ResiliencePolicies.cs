using Polly;
using Polly.Extensions.Http;
using System.Net.Http;

namespace ProductRegistrationSystemAPI.services
{
    public static class ResiliencePolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetResiliencePolicy()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .RetryAsync(3);

            var circuitBreaker = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var bulkhead = Policy.BulkheadAsync<HttpResponseMessage>(2000, int.MaxValue);

            return Policy.WrapAsync(bulkhead, Policy.WrapAsync(retry, circuitBreaker));
        }
    }
}
