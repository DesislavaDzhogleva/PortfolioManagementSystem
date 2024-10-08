﻿using Common.Configuration;
using Common.Services;
using Microsoft.Extensions.Options;
using Prices.Contracts.Services;
using Prices.Models.Events;

namespace Prices.Services
{
    public class PricePublisher : BasePublisherService<PriceUpdatedEvent>
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public PricePublisher(
            IOptions<RabbitMqSettings> rabbitMqOptions,
            ILogger<PricePublisher> logger,
            IServiceProvider serviceProvider)
                : base(rabbitMqOptions,
                       logger,
                       rabbitMqOptions.Value.Exchanges.PricesExchange.Name, 
                       rabbitMqOptions.Value.Exchanges.PricesExchange.Type)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task<IEnumerable<PriceUpdatedEvent>> GenerateEventMessagesAsync()
        {
            var priceUpdates = new List<PriceUpdatedEvent>();

            using (var scope = _serviceProvider.CreateScope())
            {
                var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
                var stocks = await stockService.GetStocksAsync();

                var random = new Random();
                foreach (var stock in stocks)
                {
                    var priceUpdate = new PriceUpdatedEvent
                    {
                        Ticker = stock.Ticker,
                        NewPrice = random.Next(10, 1500),
                        CreatedOn = DateTime.UtcNow
                    };

                    priceUpdates.Add(priceUpdate);
                }
            }

            return priceUpdates;
        }
    }
}
