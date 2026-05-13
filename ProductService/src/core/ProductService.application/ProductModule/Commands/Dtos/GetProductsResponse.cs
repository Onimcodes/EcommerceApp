using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Commands.Dtos
{
    public record GetProductsResponse(string slug, string name, string description, string imageurl, int stock, decimal price);
   
}
