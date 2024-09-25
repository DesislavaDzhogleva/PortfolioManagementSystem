using Microsoft.EntityFrameworkCore;
using Portfolios.Contracts.Repositories;
using Portfolios.Data;
using Portfolios.Data.Models;

namespace Portfolios.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PortfolioDbContext _dbContext;

        public PortfolioRepository(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PortfolioEntity>?> GetPortfoliosForUserAsync(string userId)
        {
            return await _dbContext.Portfolios
                .Where(p => p.UserId == userId)?
                .ToListAsync();
        }

        public async Task<IEnumerable<PortfolioEntity>?> GetPortfoliosForTickerAsync(string ticker)
        {
            return await _dbContext.Portfolios
                .Where(p => p.Ticker == ticker)?
                .ToListAsync();
        }

        public async Task<PortfolioEntity> GetPortfolioAsync(string userId, string ticker)
        {
            return await _dbContext.Portfolios
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Ticker == ticker);
        }

        public async Task AddPortfolioAsync(PortfolioEntity portfolio)
        {
            await _dbContext.Portfolios.AddAsync(portfolio);
        }

        public Task UpdatePortfolioAsync(PortfolioEntity portfolio)
        {
            _dbContext.Portfolios.Update(portfolio);
            return Task.CompletedTask;
        }

        public Task RemovePortfolioAsync(PortfolioEntity portfolio)
        {
            _dbContext.Portfolios.Remove(portfolio);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
