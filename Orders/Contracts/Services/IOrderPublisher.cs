using Orders.Models;

namespace Orders.Contracts.Services
{
    public interface IOrderPublisher
    {
        void PublishOrderCreatedEvent(OrderDto order);
    }
}
