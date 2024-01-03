using MediatR;
using sharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace models;
public class ProductModel : IRequest<IResult>
{
    public long ProductId { get; init; }
    public string? Name { get; init; }
    public string? StatusName { get; init; }    
    public int? Stock { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public int Discount { get; set; }
    public decimal? FinalPrice { get; set; }

    public decimal CalculateFinalPrice(int externalDiscount)
    {
        if ((!Price.HasValue) || Price.Value == 0)
        {
            return 0;            
        }

        return Price.Value * (100 - externalDiscount) / 100;
    }
}