using Common.Configuration;
using Common.Models;
using Common.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Orders.Services
{
    public class PriceSubsriber : BaseSubsriberService<BasePriceUpdatedEvent>
    {
        private readonly IMemoryCache _priceCache;

        public PriceSubsriber(
            IOptions<RabbitMqSettings> rabbitMqOptions,
            ILogger<PriceSubsriber> logger,
            IMemoryCache priceCache)
            : base(rabbitMqOptions.Value,
                   logger,
                   rabbitMqOptions.Value.Exchanges.PricesExchange.Name, 
                   rabbitMqOptions.Value.Exchanges.PricesExchange.Type)
        {
            _priceCache = priceCache;
        }

        protected override async Task ProccessMessageAsync(BasePriceUpdatedEvent priceUpdate)
        {
            _priceCache.Set(priceUpdate.Ticker, priceUpdate.NewPrice, TimeSpan.FromMinutes(10));
        }
    }
}
