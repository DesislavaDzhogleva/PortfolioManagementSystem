using Microsoft.AspNetCore.Mvc;
using Prices.Constants;
using Prices.Contracts.Services;
using Prices.Models;

namespace Prices.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStock([FromBody] StockDto stock)
        {
            try
            {
                if (string.IsNullOrEmpty(stock.Ticker) || string.IsNullOrEmpty(stock.CompanyName))
                {
                    return BadRequest(ResponseMessages.InvalidStockData);
                }

                if (await _stockService.StockExistsAsync(stock.Ticker))
                {
                    return Conflict(ResponseMessages.StockAlreadyExists);
                }

                await _stockService.AddStockAsync(stock);
                return Ok(ResponseMessages.StockAddedSuccess);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var stocks = await _stockService.GetStocksAsync();
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
