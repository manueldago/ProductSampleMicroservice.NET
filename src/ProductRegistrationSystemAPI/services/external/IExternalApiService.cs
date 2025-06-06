using ProductRegistrationSystemAPI.services.external;

namespace ProductRegistrationSystemAPI.services.external
{
    public interface IExternalApiService
    {
        Task<PostDto?> GetPostAsync(int id);
    }
}
