using Microsoft.AspNetCore.Http;
using models;
using ProductRegistrationSystemAPI.data.entities;
using sharedKernel;
using System.Net;

namespace businessLogic
{
    public interface IProductBL
    {

        Task<ResponseAPI> GetById(long? id);

        Task<ResponseAPI> Update(Product product);

        Task<ResponseAPI> Insert (Product product);

    }
}