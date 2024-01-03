using businessLogic;
using MediatR;
using ProductRegistrationSystemAPI.data.entities;
using sharedKernel;

namespace ProductRegistrationSystemAPI.data.mediator.commandHandler
{
    public class InsertRequest : IRequest<IResult>
    {
        public required Product Product { get; set; }
    }
    public class InsertCommand : IRequestHandler<InsertRequest, IResult>
    {
        private readonly IProductBL _manageProduct;

        public InsertCommand(IProductBL manageProduct)
        {
            _manageProduct = manageProduct;
        }

        public async Task<IResult> Handle(InsertRequest request, CancellationToken cancellationToken)
        {
            var response = await _manageProduct.Insert(request.Product);

            return HttpCodeHelper.Response(response);
        }
    }
}
