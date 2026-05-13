using MediatR;
using ProductService.application.Common.Dtos;
using ProductService.application.Interfaces.Persistence;
using ProductService.application.ProductModule.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, RequestResponse<List<GetProductsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<RequestResponse<List<GetProductsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await  _unitOfWork.Products.GetAllAsync();  

            var response = new RequestResponse<List<GetProductsResponse>>
            {
                ResponseCode = 200,
                ResponseMessage = "Products retrieved successfully",
                ResponseData = products.Select(p => new GetProductsResponse(slug: p.Id, name: p.Name, description : p.Description, imageurl : p.Name, stock:p.Stock, price: p.Price)).ToList()
            };
            return response;        
        }
    }
}
