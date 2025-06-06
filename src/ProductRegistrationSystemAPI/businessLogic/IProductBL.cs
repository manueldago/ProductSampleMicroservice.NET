using Microsoft.AspNetCore.Http;
using models;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.data.mediator.commandHandler;
using sharedKernel;
using System.Net;

namespace businessLogic
{
    public interface IProductBL
    {

        Task<ResponseAPI> GetById(long? id);

        Task<ResponseAPI> Update(UpdateRequest request);

        Task<ResponseAPI> Insert(InsertRequest request);

        Task<ResponseAPI> Delete(long id);

        Task<ResponseAPI> GetAll();

    }
}