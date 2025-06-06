using MediatR;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.data.mediator.commandHandler;
using ProductRegistrationSystemAPI.data.mediator.queryHandler;

namespace ProductRegistrationSystemAPI;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/product/{id}", async (long id, IMediator mediator) =>
        {
            var request = new GetRequest { ProductId = id };
            var response = await mediator.Send(request) as IResult;
            return response ?? Results.Problem();
        });

        routes.MapGet("/api/product", async (IMediator mediator) =>
        {
            var request = new GetAllRequest();
            var response = await mediator.Send(request) as IResult;
            return response ?? Results.Problem();
        });

        routes.MapPost("/api/product", async (Product product, IMediator mediator) =>
        {
            var request = new InsertRequest { Product = product };
            var response = await mediator.Send(request) as IResult;
            return response ?? Results.Problem();
        });

        routes.MapPut("/api/product/{id}", async (long id, Product product, IMediator mediator) =>
        {
            var request = new UpdateRequest { Id = id, Product = product };
            var response = await mediator.Send(request) as IResult;
            return response ?? Results.Problem();
        });

        routes.MapDelete("/api/product/{id}", async (long id, IMediator mediator) =>
        {
            var request = new DeleteRequest { Id = id };
            var response = await mediator.Send(request) as IResult;
            return response ?? Results.Problem();
        });
    }
}
