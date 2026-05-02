using MediatR;
using ProductService.application.Common.Dtos;
using ProductService.application.Interfaces.Persistence;
using ProductService.application.ProductModule.Commands.Dtos;
using ProductService.domain.Product.Models;

namespace ProductService.application.ProductModule.Commands.AddProducts
{
    public  class AddProductCommandHandler : IRequestHandler<AddProductCommand, RequestResponse<AddProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<AddProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.request.Name,
                Description = request.request.Description,
                Price = request.request.Price,
                Stock = request.request.Stock
            };


            await Task.Run(() =>  _unitOfWork.Products.Add(product));

            var response = new RequestResponse<AddProductResponse>
            {
                ResponseCode = 200,
                ResponseMessage = "Product added successfully",
                ResponseData = new AddProductResponse(ProductId : product.Id )
            };


            return response;    
          
        }
    }
}
