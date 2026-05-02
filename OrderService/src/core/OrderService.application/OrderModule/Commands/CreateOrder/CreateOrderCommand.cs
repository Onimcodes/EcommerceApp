using MediatR;
using OrderService.application.Common.Dtos;
using OrderService.application.OrderModule.Dtos;

namespace OrderService.application.OrderModule.Commands.CreateOrder
{
    public record CreateOrderCommand(CreateOrderRequest OrderCreated) : IRequest<RequestResponse<OrderCreatedResponse>>;

}
