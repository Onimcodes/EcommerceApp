using MediatR;
using Microsoft.AspNetCore.Http;
using ProductService.application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Commands.AddProductImage
{
    public record AddProductImageCommand(IFormFile imagefile) : IRequest<RequestResponse<string>>;
 
}
