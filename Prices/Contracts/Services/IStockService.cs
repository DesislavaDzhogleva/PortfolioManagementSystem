using Prices.Models;

namespace Prices.Contracts.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockInputModel>> GetStocksAsync();

        Task AddStockAsync(StockInputModel inputStock);

        Task<bool> StockExistsAsync(string ticker);
    }
}
