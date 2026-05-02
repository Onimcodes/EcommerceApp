using EcommerceEvents;
using MassTransit;
using ProductService.application.Interfaces.Persistence;

namespace ProductService.api.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderCreatedConsumer(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.WriteLine("Order received in product service");
            var msg = context.Message;
            
            foreach(var product in msg.productId)
            {
                var purchaseProduct = await unitOfWork.Products.GetByIdAsync(product);

                purchaseProduct.Stock = msg.quantity;

                 unitOfWork.Products.Update(purchaseProduct);
                
                
            }

            return;

        }
    }
}
