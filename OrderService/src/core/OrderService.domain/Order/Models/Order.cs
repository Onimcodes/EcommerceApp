using OrderService.domain.Common.Models;

namespace OrderService.domain.Order.Models
{
    public class Order : IDbEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<string> Products { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
