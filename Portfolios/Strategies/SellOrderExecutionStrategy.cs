using Common.Models;
using Portfolios.Contracts.Repositories;
using Portfolios.Contracts.Strategies;
using Portfolios.Data.Models;

namespace Portfolios.Strategies
{
    public class SellOrderExecutionStrategy : IOrderExecutionTypeStrategy
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public SellOrderExecutionStrategy(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task ExecuteAsync(PortfolioEntity portfolio, BaseOrderExecutedEvent orderExecutedEvent)
        {
            if (portfolio != null)
            {
                portfolio.NumberOfShares -= orderExecutedEvent.Quantity;
                if (portfolio.NumberOfShares <= 0)
                {
                    await _portfolioRepository.RemovePortfolioAsync(portfolio);
                }
                else
                {
                    await _portfolioRepository.UpdatePortfolioAsync(portfolio);
                }

                await _portfolioRepository.SaveChangesAsync();
            }
        }
    }

}
