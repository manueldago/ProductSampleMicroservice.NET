using businessLogic;
using MediatR;
using ProductRegistrationSystemAPI.data.entities;
using sharedKernel;

namespace ProductRegistrationSystemAPI.data.mediator.commandHandler
{
    public class UpdateRequest : IRequest<IResult>
    {
        public required long Id { get; set; }
        public required Product Product { get; set; }
    }

    public class UpdateCommand : IRequestHandler<UpdateRequest, IResult>
    {
        private readonly IProductBL _manageProduct;

        public UpdateCommand(IProductBL manageProduct)
        {
            _manageProduct = manageProduct;
        }

        public async Task<IResult> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            var response = await _manageProduct.Update(request);

            return HttpCodeHelper.Response(response);
        }
    }
}
