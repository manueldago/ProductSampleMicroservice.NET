﻿using businessLogic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using models;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.data.mediator.commandHandler;
using ProductRegistrationSystemAPI.data.mediator.queryHandler;
using services;
using sharedKernel;
using System.Net;

namespace ProductRegistrationSystemAPI.controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBL _manageProduct;
        private readonly IMediator _mediator;

        public ProductController(IProductBL manageProduct, IMediator mediator)
        {
            _manageProduct = manageProduct;
            _mediator = mediator;            
        }

        [HttpGet]
        [Route("/api/product/{id}")]
        public async Task<IResult> GetById(long? id)
        {
            var request = new GetRequest() { ProductId = id };

            var response = await _mediator.Send(request) as IResult;

            return response == null ? Results.Json(Problem()) : response;
        }


        [HttpPut]
        [Route("/api/product/{id}")]
        public async Task<IResult> Update(long id, [FromBody] Product product)
        {
            var request = new UpdateRequest() { Id= id, Product = product };

            var response = await _mediator.Send(request) as IResult;

            return response == null ? Results.Json(Problem()) : response;            
        }

        [HttpPost]
        [Route("/api/product")]
        public async Task<IResult> Insert([FromBody] Product product)
        {
            var request = new InsertRequest() { Product = product };

            var response = await _mediator.Send(request) as IResult;

            return response == null ? Results.Json(Problem()) : response;
        }

    }
}
