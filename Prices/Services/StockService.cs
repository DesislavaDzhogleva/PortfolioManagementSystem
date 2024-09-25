using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Prices.Constants;
using Prices.Contracts.Repostories;
using Prices.Contracts.Services;
using Prices.Data.Models;
using Prices.Models;

namespace Prices.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public StockService(
            IStockRepository stockRepository,
            IMemoryCache memoryCache,
            IMapper mapper)
        {
            _stockRepository = stockRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public async Task<bool> StockExistsAsync(string ticker)
        {
            return await _stockRepository.StockExistsAsync(ticker);
        }

        public async Task AddStockAsync(StockDto inputStock)
        {
            var stockEntity = _mapper.Map<StockEntity>(inputStock);
            await _stockRepository.AddStockAsync(stockEntity);
        }

        public async Task<IEnumerable<StockDto>> GetStocksAsync()
        {
            if (!_memoryCache.TryGetValue(CacheConstants.StocksCacheKey, out IEnumerable<StockDto> stocks))
            {
                var stockEntities = await _stockRepository.GetAllStocksAsync();
                stocks = _mapper.Map<IEnumerable<StockDto>>(stockEntities);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _memoryCache.Set(CacheConstants.StocksCacheKey, stocks, cacheEntryOptions);
            }

            return stocks;
        }
    }
}
