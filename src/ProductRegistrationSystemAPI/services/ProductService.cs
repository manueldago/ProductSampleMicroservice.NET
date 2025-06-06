using models;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.data.repositories;


namespace services;

public class ProductService : IProductService
{
    
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;        
    }

    public Task<ProductModel?> GetById(long id)
    {       
        var product = _productRepository.GetById(id);
        return Task.FromResult(product);
    }

    public Task<bool> Update(Product product, long id)
    {
        var productFromDB = _productRepository.GetById(id);
        if (productFromDB == null)
        {
            return Task.FromResult(false);
        } 
        
        product.ProductId = productFromDB.ProductId;
        bool result =_productRepository.Update(product);
        
        return Task.FromResult(result);
    }
    public Task<long> Insert(Product product)
    {
        long result = _productRepository.Insert(product);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<ProductModel>> GetAll()
    {
        var products = _productRepository.GetAll();
        return Task.FromResult(products);
    }

    public Task<bool> Delete(long id)
    {
        bool result = _productRepository.Delete(id);
        return Task.FromResult(result);
    }
}