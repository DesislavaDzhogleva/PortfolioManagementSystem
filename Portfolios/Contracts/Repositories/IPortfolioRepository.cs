using Portfolios.Data.Models;

namespace Portfolios.Contracts.Repositories
{
    public interface IPortfolioRepository
    {
        Task<PortfolioEntity> GetPortfolioAsync(string userId, string ticker);

        Task<IEnumerable<PortfolioEntity>?> GetPortfoliosForTickerAsync(string ticker);

        Task<IEnumerable<PortfolioEntity>?> GetPortfoliosForUserAsync(string userId);

        Task AddPortfolioAsync(PortfolioEntity portfolio);

        Task UpdatePortfolioAsync(PortfolioEntity portfolio);

        Task RemovePortfolioAsync(PortfolioEntity portfolio);

        Task SaveChangesAsync();
    }
}
