using Common.Configuration;
using Common.Services;
using Microsoft.Extensions.Options;
using Portfolios.Contracts;
using Portfolios.Models.Events;

namespace Portfolios.Services
{
    public class OrderEventSubcriber : BaseSubsriberService<OrderExecutedEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderEventSubcriber(
            IOptions<RabbitMqSettings> rabbitMqOptions,
            ILogger<OrderEventSubcriber> logger,
            IServiceProvider serviceProvider)
            : base(rabbitMqOptions.Value,
                   logger,
                   rabbitMqOptions.Value.Exchanges.OrderExchange.Name,
                   rabbitMqOptions.Value.Exchanges.OrderExchange.Type)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ProccessMessageAsync(OrderExecutedEvent orderEvent)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var portfolioService = scope.ServiceProvider.GetRequiredService<IPortfolioService>();

                await portfolioService.HandleOrderExecuted(orderEvent);
            }
        }
    }
}
