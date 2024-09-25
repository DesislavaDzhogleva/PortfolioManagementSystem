using Prices.Models;

namespace Prices.Contracts.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockDto>> GetStocksAsync();

        Task AddStockAsync(StockDto inputStock);

        Task<bool> StockExistsAsync(string ticker);
    }
}
