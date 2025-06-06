using models;
using ProductRegistrationSystemAPI.data.entities;

namespace services;

public interface IProductService
{
    public Task<ProductModel?> GetById(long id);
    public Task<bool> Update(Product product, long id);
    public Task<long> Insert(Product product);
    public Task<IEnumerable<ProductModel>> GetAll();
    public Task<bool> Delete(long id);
}