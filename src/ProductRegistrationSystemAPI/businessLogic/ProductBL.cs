using MediatR;
using models;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.services;
using services;
using sharedKernel;
using System.Net;

namespace businessLogic 
{
    public class ProductBL : IProductBL
    {
        private readonly IProductService _productService;
        private readonly IDiscountExternalService _discountExternalService;        
        private readonly HttpStatusCode _statusCode;

        public ProductBL (IProductService productService, 
                          IDiscountExternalService discountExternalService,
                          IHttpContextAccessor? httpContextAccessor)
        {
            _productService = productService;
            _discountExternalService = discountExternalService;            
            if (httpContextAccessor?.HttpContext?.Response?.StatusCode == null)
            {
                _statusCode = HttpStatusCode.NoContent;
            }
            else
            {                
                _statusCode = (HttpStatusCode)httpContextAccessor.HttpContext.Response.StatusCode;
            }                        
        }

        public async Task<ResponseAPI> GetById(long? id)
        {
            var responseMask = new ResponseAPI();

            try
            {
                if (!id.HasValue)
                {
                    responseMask.ResponseData = "Must be valid id for complete to request.";
                    throw new ApplicationException("Error on Id parameter!");
                }

                var productModel = await _productService.GetById(id.Value);
                if (productModel == null)
                {
                    throw new ArgumentNullException("Product not exist in our database.");
                }

                await CalculatePrice(productModel);


                responseMask.ResponseStatusCode =_statusCode;
                responseMask.ResponseData = productModel;

            }
            catch (Exception ex)
            {
                responseMask.ResponseException = new ExceptionAPI() { Message = ex.Message, Source = "ProductController.GetById" };                  
            }

            return responseMask;
        }
     
        public async Task<ResponseAPI> Insert(Product product)
        {
            var responseMask = new ResponseAPI();
            try
            {
                if (product.ProductId != 0)
                { 
                   throw new ArgumentNullException("Insert product do not needs the property ProductId with value");
                }

                long result = await _productService.Insert(product);
                string dataResult = result > 0 ? "Product has been added on database succesfully" : "Product do not added on database.";
                responseMask.ResponseData = $"New id generated for added product is: {result}";
                responseMask.ResponseStatusCode = _statusCode;
            }
            catch (Exception ex)
            {
                responseMask.ResponseException = new ExceptionAPI() { Message = ex.Message, Source = "ProductController.GetById" };                
            }

            return responseMask;
        }

        public async Task<ResponseAPI> Update(Product product)
        {
            var responseMask = new ResponseAPI();

            try
            {

                bool result = await _productService.Update(product);
                string dataResult = result ? "Product has been updated on database succesfully" : "Product do not updated on database.";
                responseMask.ResponseData = dataResult;
                responseMask.ResponseStatusCode = _statusCode;
            }
            catch (Exception ex)
            {
                responseMask.ResponseException = new ExceptionAPI() { Message = ex.Message, Source = "ProductController.GetById" };                
            }

            return responseMask;
        }

        private async Task CalculatePrice(ProductModel productModel)
        {
            int discountFromService = await _discountExternalService.GetDiscount();
            productModel.Discount = discountFromService;
            productModel.FinalPrice = productModel.CalculateFinalPrice(discountFromService);
        }
    }
}

