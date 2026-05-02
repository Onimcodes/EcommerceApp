using MediatR;
using ProductService.application.Common.Dtos;
using ProductService.application.ProductModule.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Commands.AddProducts
{
    public record AddProductCommand(AddProductRequest request) : IRequest<RequestResponse<AddProductResponse>>;
  

}
