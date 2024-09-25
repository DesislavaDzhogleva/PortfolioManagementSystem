using Prices.Data.Models;

namespace Prices.Contracts.Repostories
{
    public interface IStockRepository
    {
        Task<List<StockEntity>> GetAllStocksAsync();

        Task AddStockAsync(StockEntity stock);

        Task<bool> StockExistsAsync(string ticker);
    }
}
