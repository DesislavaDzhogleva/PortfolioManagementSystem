using Common.Configuration;
using Common.Services;
using Microsoft.Extensions.Options;
using Orders.Contracts.Services;
using Orders.Models;
using Orders.Models.Events;

namespace Orders.Services
{
    public class OrderPublisher : BaseNotificationService<OrderExecuteEvent>, IOrderPublisher
    {
        public OrderPublisher(
            IOptions<RabbitMqSettings> rabbitMqOptions) 
                : base(rabbitMqOptions,
                       rabbitMqOptions.Value.Exchanges.OrderExchange.Name,
                       rabbitMqOptions.Value.Exchanges.OrderExchange.Type)
        {
        }

        public void PublishOrderCreatedEvent(OrderDto order)
        {
            var orderCreatedEvent = new OrderExecuteEvent
            {
                Price = order.OrderedPrice,
                Quantity = order.Quantity,
                Side = order.Side,
                Ticker = order.Ticker,
                UserId = order.UserId,
            };

            PublishEvent(orderCreatedEvent);
        }
    }
}
