using Portfolios.Models.Events;
using Portfolios.Models.Responses;

namespace Portfolios.Contracts
{
    public interface IPortfolioService
    {
        Task<UserPortfolioInvestmentResponse> GetPortfolioValue(string userId);

        Task HandleOrderExecuted(OrderExecutedEvent orderExecutedEvent);

        Task HandlePriceUpdate(PriceUpdatedEvent priceUpdatedEvent);
    }
}
