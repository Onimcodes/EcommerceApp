using ProductService.domain.Product.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.Interfaces.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
