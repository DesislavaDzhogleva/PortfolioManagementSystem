using Orders.Data.Models;
using Orders.Models;

namespace Orders.Contracts.Services
{
    public interface IOrderService
    {
        Task<OrderDto> SubmitOrderAsync(string userId, OrderRequest request, decimal latestPrice);
    }
}
