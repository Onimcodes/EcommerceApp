using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.application.OrderModule.Dtos
{
    public record CreateOrderRequest(decimal totalAmount, List<string> productIds, string customerId);
}
