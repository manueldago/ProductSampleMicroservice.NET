using ProductRegistrationSystemAPI.services.external;

namespace ProductRegistrationSystemAPI.services.external
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly IJsonPlaceholderApi _api;

        public ExternalApiService(IJsonPlaceholderApi api)
        {
            _api = api;
        }

        public async Task<PostDto?> GetPostAsync(int id)
        {
            try
            {
                return await _api.GetPostAsync(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
