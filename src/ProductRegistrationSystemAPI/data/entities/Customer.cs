using System.ComponentModel.DataAnnotations;

namespace ProductRegistrationSystemAPI.data.entities;

public class Customer
{
    [Key]
    public long CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
