using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Orders.Contracts.Services;
using Orders.Models;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMemoryCache _memoryCache;
        private readonly IOrderPublisher _orderPublisher;

        public OrderApiController(
            IOrderService orderService,
            IMemoryCache memoryCache,
            IOrderPublisher orderPublisher)
        {
            _orderService = orderService;
            _memoryCache = memoryCache;
            _orderPublisher = orderPublisher;
        }

        // POST /api/order/add/{userId}
        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddOrder([FromRoute] string userId, [FromBody] OrderRequest orderRequest)
        {
            try
            {
                if (!_memoryCache.TryGetValue(orderRequest.Ticker, out decimal latestPrice))
                {
                    return BadRequest($"Price not available for the ticker {orderRequest.Ticker}. " +
                        $"Try again later.");
                }

                var order = await _orderService.SubmitOrderAsync(userId, orderRequest, latestPrice);
                _orderPublisher.PublishOrderCreatedEvent(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
