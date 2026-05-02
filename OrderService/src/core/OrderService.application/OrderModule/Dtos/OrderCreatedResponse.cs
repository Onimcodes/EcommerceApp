using EcommerceEvents;
using MediatR;

namespace OrderService.application.OrderModule.Dtos
{
    public record OrderCreatedResponse(string OrderId);
 
}
