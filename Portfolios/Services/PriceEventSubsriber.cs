using Common.Configuration;
using Common.Services;
using Portfolios.Contracts;
using Portfolios.Models.Events;
using Microsoft.Extensions.Options;


namespace Portfolios.Services
{
    public class PriceEventSubsriber : BaseSubsriberService<PriceUpdatedEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public PriceEventSubsriber(
            IOptions<RabbitMqSettings> rabbitMqOptions,
            IServiceProvider serviceProvider)
            : base(rabbitMqOptions.Value,
                    rabbitMqOptions.Value.Exchanges.PricesExchange.Name,
                    rabbitMqOptions.Value.Exchanges.PricesExchange.Type)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ProccessMessageAsync(PriceUpdatedEvent priceEvent)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var portfolioService = scope.ServiceProvider.GetRequiredService<IPortfolioService>();

                await portfolioService.HandlePriceUpdate(priceEvent);
            }
        }
    }
}

