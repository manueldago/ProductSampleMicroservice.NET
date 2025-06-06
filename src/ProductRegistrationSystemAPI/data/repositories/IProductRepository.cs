using models;
using ProductRegistrationSystemAPI.data.entities;

namespace ProductRegistrationSystemAPI.data.repositories
{
    public interface IProductRepository
    {
        ProductModel? GetById(long? id);

        bool Update(Product product);

        long Insert(Product product);

        IEnumerable<ProductModel> GetAll();

        bool Delete(long id);
    }
}
