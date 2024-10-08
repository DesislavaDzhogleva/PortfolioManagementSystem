﻿using Common.Models;
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
                    // Remove the portfolio if the number of shares is less than or equal to 0
                    // Maybe it should be returned how many shares can the user currently can sell and update the order 
                    _portfolioRepository.RemovePortfolio(portfolio);
                }
                else
                {
                    _portfolioRepository.UpdatePortfolio(portfolio);
                }

                await _portfolioRepository.SaveChangesAsync();
            }
        }
    }

}
