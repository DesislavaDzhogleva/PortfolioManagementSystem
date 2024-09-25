using Common.Models;
using Portfolios.Data.Models;

namespace Portfolios.Contracts.Strategies
{
    public interface IOrderExecutionTypeStrategy
    {
        Task ExecuteAsync(PortfolioEntity portfolio, BaseOrderExecutedEvent orderExecutedEvent);
    }
}
