using businessLogic;
using MediatR;
using sharedKernel;

namespace ProductRegistrationSystemAPI.data.mediator.commandHandler;

public class DeleteRequest : IRequest<IResult>
{
    public required long Id { get; set; }
}

public class DeleteCommand : IRequestHandler<DeleteRequest, IResult>
{
    private readonly IProductBL _manageProduct;

    public DeleteCommand(IProductBL manageProduct)
    {
        _manageProduct = manageProduct;
    }

    public async Task<IResult> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
        var response = await _manageProduct.Delete(request.Id);
        return HttpCodeHelper.Response(response);
    }
}
