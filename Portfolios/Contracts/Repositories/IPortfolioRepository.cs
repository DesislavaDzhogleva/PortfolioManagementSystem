using Portfolios.Data.Models;

namespace Portfolios.Contracts.Repositories
{
    public interface IPortfolioRepository
    {
        Task<PortfolioEntity> GetPortfolioAsync(string userId, string ticker);

        Task<IEnumerable<PortfolioEntity>?> GetPortfoliosForTickerAsync(string ticker);

        Task<IEnumerable<PortfolioEntity>?> GetPortfoliosForUserAsync(string userId);

        Task AddPortfolioAsync(PortfolioEntity portfolio);

        void UpdatePortfolio(PortfolioEntity portfolio);

        void RemovePortfolio(PortfolioEntity portfolio);

        Task SaveChangesAsync();
    }
}
