using Portfolios.Contracts.Strategies;

namespace Portfolios.Strategies.Factory
{
    public class OrderTypeStrategyFactory
    {
        private readonly IEnumerable<IOrderExecutionTypeStrategy> _strategies;

        public OrderTypeStrategyFactory(IEnumerable<IOrderExecutionTypeStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IOrderExecutionTypeStrategy GetStrategy(string side)
        {
            return _strategies.FirstOrDefault(strategy =>
                strategy.GetType().Name.StartsWith(side, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException($"No strategy found for side: {side}");
        }

    }
}
