using businessLogic;
using MediatR;
using sharedKernel;

namespace ProductRegistrationSystemAPI.data.mediator.queryHandler;

public class GetAllRequest : IRequest<IResult>
{
}

public class GetAllQuery : IRequestHandler<GetAllRequest, IResult>
{
    private readonly IProductBL _manageProduct;

    public GetAllQuery(IProductBL manageProduct)
    {
        _manageProduct = manageProduct;
    }

    public async Task<IResult> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var response = await _manageProduct.GetAll();
        return HttpCodeHelper.Response(response);
    }
}
