using AutoMapper;
using Common.Models;
using Portfolios.Contracts.Repositories;
using Portfolios.Contracts.Strategies;
using Portfolios.Data.Models;

namespace Portfolios.Strategies
{
    public class BuyOrderExecutionStrategy : IOrderExecutionTypeStrategy
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public BuyOrderExecutionStrategy(
            IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task ExecuteAsync(PortfolioEntity portfolio, BaseOrderExecutedEvent orderExecutedEvent)
        {
            if (portfolio == null)
            {
                portfolio = new PortfolioEntity
                {
                    UserId = orderExecutedEvent.UserId,
                    Ticker = orderExecutedEvent.Ticker,
                    NumberOfShares = orderExecutedEvent.Quantity,
                    AveragePrice = orderExecutedEvent.Price,
                    CurrentPrice = orderExecutedEvent.Price,
                    UpdatedOn = DateTime.UtcNow,
                };

                await _portfolioRepository.AddPortfolioAsync(portfolio);
            }
            else
            {
                portfolio.NumberOfShares += orderExecutedEvent.Quantity;
                portfolio.AveragePrice = ((portfolio.AveragePrice * portfolio.NumberOfShares) + (orderExecutedEvent.Price * orderExecutedEvent.Quantity)) / (portfolio.NumberOfShares + orderExecutedEvent.Quantity);

                _portfolioRepository.UpdatePortfolio(portfolio);
            }

            await _portfolioRepository.SaveChangesAsync();
        }
    }

}
