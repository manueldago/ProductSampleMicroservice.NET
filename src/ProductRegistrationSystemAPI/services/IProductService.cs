using models;
using ProductRegistrationSystemAPI.data.entities;

namespace services;

public interface IProductService
{
    public Task<ProductModel?> GetById(long id);
    public Task<bool> Update(Product product);
    public Task<long> Insert(Product product);
}