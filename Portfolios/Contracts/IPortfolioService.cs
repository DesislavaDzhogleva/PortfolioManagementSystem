using Portfolios.Models.Events;

namespace Portfolios.Contracts
{
    public interface IPortfolioService
    {
        Task HandleOrderExecuted(OrderExecutedEvent orderExecutedEvent);

        Task HandlePriceUpdate(PriceUpdatedEvent priceUpdatedEvent);
    }
}
