using Microsoft.EntityFrameworkCore.Storage.Json;
using models;
using ProductRegistrationSystemAPI.data.cache;
using ProductRegistrationSystemAPI.data.context;
using ProductRegistrationSystemAPI.data.entities;
using System.Reflection.Metadata.Ecma335;


namespace ProductRegistrationSystemAPI.data.repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        private readonly IProductCache _productCache;

        public ProductRepository(ProductContext context, IProductCache productCache)
        {
            _context = context;
            _productCache = productCache;
        }

        public ProductModel? GetById(long? id)
        {
            try
            {
                var product = _context.Product.Where(p => p.ProductId.Equals(id)).FirstOrDefault();
                if (product == null) return null;

                ProductModel? model = new ProductModel()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,                  
                    StatusName = _productCache.GetStatusName(product.Status),
                    Stock = product.Stock,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = 0,
                    FinalPrice = 0,                    
                };

                return model;
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public long Insert(Product product)
        {
            try
            {                
                _context.Product.Add(product);
                _context.SaveChanges();

                return product.ProductId;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Update(Product product)
        {
            try
            {

                var productFromDB = _context.Product.Where(p => p.ProductId.Equals(product.ProductId)).FirstOrDefault();
                if (productFromDB == null) throw new ArgumentNullException(nameof(product));

                productFromDB.Name = product.Name;
                productFromDB.Price = product.Price;
                productFromDB.Status = product.Status;
                productFromDB.Description = product.Description;
                productFromDB.Stock = product.Stock;
               
                _context.Product.Update(productFromDB);
                _context.SaveChanges();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
