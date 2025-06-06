using Microsoft.VisualStudio.TestTools.UnitTesting;
using businessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductRegistrationSystemAPI.controllers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using models;
using ProductRegistrationSystemAPI.data.mediator.queryHandler;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using ProductRegistrationSystemAPI.data.cache;
using ProductRegistrationSystemAPI.data.context;
using ProductRegistrationSystemAPI.data.repositories;
using ProductRegistrationSystemAPI.services;
using services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Http;
using sharedKernel;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.data.mediator.commandHandler;

namespace ProductRegistrationSystemAPITest.tests
{
    [TestClass()]
    public class ProductTest
    {

        public required IServiceScope _scope;
        public required IProductBL _productBL;
        public required IMediator _mediator;
        public required IHttpContextAccessor _statusCode;

        public ProductTest()
        {
            InitialSetup();
        }

        #region GetById

        [TestMethod()]
        public async Task GetById_FailIfHaveBadProductQuery_Value()
        {
            // Arrange
            var productQuery = new GetRequest { ProductId = 0 };

            // Act        
            var product = await _productBL.GetById(productQuery.ProductId);


            // Assert                  
            Assert.IsNull(product.ResponseData);

        }

        [TestMethod()]
        public async Task GetById_ShouldHaveProductModel_Entity()
        {
            // Arrange
            var productQuery = new GetRequest { ProductId = 1 };

            // Act        
            var product = await _productBL.GetById(productQuery.ProductId);


            // Assert                  
            Assert.IsNotNull(product);
            Assert.IsInstanceOfType(product, typeof(ResponseAPI));
        }

        #endregion


        #region Insert

        [TestMethod()]
        public async Task Insert_FailIfHaveEqual_Id_FromOtherProduct_Inserted_On_DB()
        {
            // Arrange
            var insertRequest = new InsertRequest()
            {
                Product = new Product()
                {
                    ProductId = 1,
                    Description = "PC NOTEBOOK",
                    Name = "PC",
                    Price = 10,
                    Status = 1,
                    Stock = 10
                }
            };

            // Act        
            var product = await _productBL.Insert(insertRequest);

            // Assert                  
            Assert.IsNull(product.ResponseData);

        }

        [TestMethod()]
        public async Task Insert_ShouldGetNewIdProduct_To_Inserted()
        {
            var insertRequest = new InsertRequest()
            {
                Product = new Product()
                {
                    Description = "PC NOTEBOOK",
                    Name = "PC",
                    Price = 10,
                    Status = 1,
                    Stock = 10
                }
            };

            // Act        
            var product = await _productBL.Insert(insertRequest);

            // Assert                  
            Assert.IsTrue(product.ResponseData?.ToString()?.Contains("New id generated for added product is"));
        }

        #endregion


        #region Update

        [TestMethod()]
        public async Task Update_FailIfHaveProductId_that_notInsertedYet()
        {
            var updateRequest = new UpdateRequest()
            {
                Product= new Product()
                {                    
                    Description = "MOUSE WIRELESS",
                    Name = "MOUSE",
                    Price = 100,
                    Status = 0,
                    Stock = 100
                },
                Id= 9999
            };

            // Act
            var product = await _productBL.Update(updateRequest);

            // Assert
            Assert.IsNull(product.ResponseException);
            Assert.AreEqual("Product not updated in database.", product.ResponseData);
            Assert.AreNotEqual(HttpStatusCode.OK, product.ResponseStatusCode);

        }

        [TestMethod()]
        public async Task Update_ShouldHaveStatusOK_When_ProductHasBeenUpdated()
        {
            var updateRequest = new UpdateRequest()
            {
                Product = new Product()
                {                    
                    Description = "PHILIPS TV",
                    Name = "SMART TV",
                    Price = 100,
                    Status = 1,
                    Stock = 10
                },
                Id= 1
            };

            // Act        
            var product = await _productBL.Update(updateRequest);


            // Assert                  
            Assert.IsTrue(product.ResponseData?.ToString()?.Contains("Product has been updated on database successfully"));
        }

        #endregion

        private void InitialSetup()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddDbContext<ProductContext>(options => options.UseSqlite("Data Source=data/sqlliteDB/product-register.db"));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductBL, ProductBL>();
            services.AddScoped<ICacheHelper, CacheHelper>();
            services.AddScoped<IProductCache, ProductCache>();
            services.AddHttpClient<DiscountExternalservice>();
            services.AddScoped<IDiscountExternalService, DiscountExternalservice>();
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());


            var serviceProvider = services.BuildServiceProvider();
            _scope = serviceProvider.CreateScope();
            _statusCode = _scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            _productBL = _scope.ServiceProvider.GetRequiredService<IProductBL>();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }
    }
}