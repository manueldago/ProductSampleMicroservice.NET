using businessLogic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductRegistrationSystemAPI.data.cache;
using ProductRegistrationSystemAPI.data.context;
using ProductRegistrationSystemAPI.data.repositories;
using ProductRegistrationSystemAPI.services;
using ProductRegistrationSystemAPI.sharedKernel;
using ProductRegistrationSystemAPI;
using ProductRegistrationSystemAPI.validators;
using services;
using System.Reflection;
using ProductRegistrationSystemAPI.mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite("Data Source=data/sqlliteDB/product-register.db"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<ICacheHelper, CacheHelper>();
builder.Services.AddScoped<IProductCache, ProductCache>();
builder.Services.AddHttpClient<DiscountExternalservice>();
builder.Services.AddScoped<IDiscountExternalService,DiscountExternalservice>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(c=> c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.MapProductEndpoints();
app.Run();
