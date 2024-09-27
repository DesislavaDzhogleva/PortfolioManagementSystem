using Microsoft.EntityFrameworkCore;
using Portfolios.Contracts;
using Portfolios.Contracts.Repositories;
using Portfolios.Models.Events;
using Portfolios.Models.Responses;
using Portfolios.Strategies.Factory;

namespace Portfolios.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly OrderTypeStrategyFactory _orderTypeStrategyFactory;

        public PortfolioService(
            IPortfolioRepository portfolioRepository,
            OrderTypeStrategyFactory orderTypeStrategyFactory)
        {
            _portfolioRepository = portfolioRepository;
            _orderTypeStrategyFactory = orderTypeStrategyFactory;
        }

        public async Task<UserPortfolioInvestmentResponse> GetPortfolioValue(string userId)
        {
            var userPortofios = await _portfolioRepository
                .GetPortfoliosForUserAsync(userId);

            if (userPortofios != null && userPortofios.Any())
            {
                var response = UserPortfolioInvestmentResponse.BaseResponse(userId);
                decimal totalPortfolioValue = userPortofios.Sum(i => i.TotalInvestment);
                response.Investment = totalPortfolioValue;
                return response;
            }
            else
            {
                var response = UserPortfolioInvestmentResponse.EmptyResponse(userId);
                return response;
            }
        }

        public async Task HandleOrderExecuted(OrderExecutedEvent orderExecutedEvent)
        {
            var type = orderExecutedEvent.Side;

            var portfolioEntity = await _portfolioRepository
                .GetPortfolioAsync(orderExecutedEvent.UserId, orderExecutedEvent.Ticker);

            var strategy = _orderTypeStrategyFactory.GetStrategy(type);
            if(strategy != null)
            {
                await strategy.ExecuteAsync(portfolioEntity, orderExecutedEvent);
            }
        }

        public async Task HandlePriceUpdate(PriceUpdatedEvent priceUpdatedEvent)
        {
            var ticker = priceUpdatedEvent.Ticker;
            var newPrice = priceUpdatedEvent.NewPrice;

            var portfolios = await _portfolioRepository.GetPortfoliosForTickerAsync(ticker);

            if (portfolios != null && portfolios.Any())
            {
                foreach (var portfolio in portfolios)
                {
                    portfolio.CurrentPrice = newPrice;
                    portfolio.UpdatedOn = DateTime.UtcNow;
                     _portfolioRepository.UpdatePortfolio(portfolio);
                }

                await _portfolioRepository.SaveChangesAsync();
            }
        }
    }
}
