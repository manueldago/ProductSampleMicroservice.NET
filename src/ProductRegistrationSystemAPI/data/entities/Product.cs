using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductRegistrationSystemAPI.data.entities
{
    public class Product : IRequest<IResult>
    {
        [Key]
        public long ProductId { get; set; }
        public string? Name { get; set; }
        public byte Status { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
