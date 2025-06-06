using Microsoft.EntityFrameworkCore.Storage.Json;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductRepository(ProductContext context, IProductCache productCache, IMapper mapper)
        {
            _context = context;
            _productCache = productCache;
            _mapper = mapper;
        }

        public ProductModel? GetById(long? id)
        {
            try
            {
            var product = _context.Product.Where(p => p.ProductId.Equals(id)).FirstOrDefault();
            if (product == null) return null;

            var model = _mapper.Map<ProductModel>(product);
            model.StatusName = _productCache.GetStatusName(product.Status);
            model.Discount = 0;
            model.FinalPrice = 0;

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

        public IEnumerable<ProductModel> GetAll()
        {
            try
            {
                return _context.Product
                    .Select(p =>
                    {
                        var model = _mapper.Map<ProductModel>(p);
                        model.StatusName = _productCache.GetStatusName(p.Status);
                        model.Discount = 0;
                        model.FinalPrice = 0;
                        return model;
                    }).ToList();
            }
            catch (Exception)
            {
                return Enumerable.Empty<ProductModel>();
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var product = _context.Product.FirstOrDefault(p => p.ProductId == id);
                if (product == null) return false;
                _context.Product.Remove(product);
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
