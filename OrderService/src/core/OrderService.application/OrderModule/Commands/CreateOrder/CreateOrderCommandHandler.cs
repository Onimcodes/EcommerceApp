using EcommerceEvents;
using MassTransit;
using MediatR;
using OrderService.application.Common.Dtos;
using OrderService.application.Interfaces.Persistence;
using OrderService.application.OrderModule.Dtos;
using OrderService.domain.Order.Models;

namespace OrderService.application.OrderModule.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, RequestResponse<OrderCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateOrderCommandHandler(IUnitOfWork unitOfwork, IPublishEndpoint publishEndpoint)
        {
            _unitOfwork = unitOfwork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<RequestResponse<OrderCreatedResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            request.Deconstruct(out var orderRequest);

            var newOrder = new Order
            {
                UserId = orderRequest.customerId,
                Products = orderRequest.productIds,
                Total = orderRequest.totalAmount
            };


           _unitOfwork.Orders.Add(newOrder);


            var orderCreatedEvent = new OrderCreated(productId: orderRequest.productIds, quantity: 45, price: 230, customerId: orderRequest.customerId, orderDate: DateTime.UtcNow); //stream a list of products with individual quantity instead e.g{productId: kssks, quantity: 3}

            await _publishEndpoint.Publish(orderCreatedEvent);


            return new RequestResponse<OrderCreatedResponse>
            {
                ResponseCode = 200,
                ResponseData = new OrderCreatedResponse(OrderId: newOrder.Id)
            };

        }
    }
}


