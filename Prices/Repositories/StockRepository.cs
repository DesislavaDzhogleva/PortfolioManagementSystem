using Microsoft.EntityFrameworkCore;
using Prices.Contracts.Repostories;
using Prices.Data;
using Prices.Data.Models;

namespace Prices.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly PriceDbContext _context;

        public StockRepository(PriceDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockEntity>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task AddStockAsync(StockEntity stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> StockExistsAsync(string ticker)
        {
            return await _context.Stocks.AnyAsync(s => s.Ticker == ticker);
        }
    }
}
