using Microsoft.Extensions.Caching.Memory;
using Orders.Contracts.Services;
using Orders.Data.Models.Enums;
using Orders.Data;
using Orders.Data.Models;
using Orders.Models;
using AutoMapper;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(
            OrderDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> SubmitOrderAsync(string userId, OrderRequest request, decimal latestPrice)
        {
            Enum.TryParse<OrderSide>(request.Side, out var type);
            var order = new OrderEntity
            {
                UserId = userId,
                Ticker = request.Ticker,
                Quantity = request.Quantity,
                Side = type,
                OrderedPrice = latestPrice,
                CreatedOn = DateTime.UtcNow
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }
    }
}
