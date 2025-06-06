using Refit;

namespace ProductRegistrationSystemAPI.services.external
{
    public interface IJsonPlaceholderApi
    {
        [Get("/posts/{id}")]
        Task<PostDto> GetPostAsync(int id);
    }

    public class PostDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
