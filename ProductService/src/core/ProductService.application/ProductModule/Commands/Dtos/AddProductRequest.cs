using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.ProductModule.Commands.Dtos
{
    public record AddProductRequest
        (
        string Name,
        string Description,
        int Stock,
        decimal Price

        );

   
}
