using System.ComponentModel.DataAnnotations;

namespace ProductRegistrationSystemAPI.data.entities;

public class Catalog
{
    [Key]
    public long CatalogId { get; set; }
    public string? Name { get; set; }
}
