using ProductService.application.Interfaces.Persistence;
using ProductService.domain.Product.Models;
using ProductService.infrastructure.Common.DataContext;

namespace ProductService.infrastructure.Persistence
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
