using businessLogic;
using MediatR;
using models;
using sharedKernel;
using System.Data.SqlTypes;
using System.Net;

namespace ProductRegistrationSystemAPI.data.mediator.queryHandler
{
    public class ProductQuery : IRequest<IResult>
    {
        public long? ProductId { get; set; }
    }

    public class GetByIdQuery : IRequestHandler<ProductQuery, IResult>
    {
        private readonly IProductBL _manageProduct;

        public GetByIdQuery(IProductBL manageProduct)
        {
            _manageProduct = manageProduct;
        }

        public async Task<IResult> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var response = await _manageProduct.GetById(request.ProductId);

            return HttpCodeHelper.Response(response);
        }
    }
}
