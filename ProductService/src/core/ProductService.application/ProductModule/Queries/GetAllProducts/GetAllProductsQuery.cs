using MediatR;
using ProductService.application.Common.Dtos;
using ProductService.application.ProductModule.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Queries.GetAllProducts
{
    public record GetAllProductsQuery() : IRequest<RequestResponse<List<GetProductsResponse>>>;

}
