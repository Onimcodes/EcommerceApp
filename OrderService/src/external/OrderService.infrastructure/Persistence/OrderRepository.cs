using OrderService.application.Interfaces.Persistence;
using OrderService.domain.Order.Models;
using OrderService.infrastructure.Common.DataContext;

namespace OrderService.infrastructure.Persistence
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
