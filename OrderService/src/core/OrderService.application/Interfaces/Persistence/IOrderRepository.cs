using OrderService.domain.Order.Models;

namespace OrderService.application.Interfaces.Persistence
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}
