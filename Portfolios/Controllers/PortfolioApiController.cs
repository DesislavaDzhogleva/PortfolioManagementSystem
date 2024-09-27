using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Portfolios.Contracts;

namespace Portfolios.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioApiController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioApiController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetPortfolio([FromRoute] string userId)
        {
            try
            {
               var portfolioResult =  await _portfolioService.GetPortfolioValue(userId);
                if (!portfolioResult.Exists) 
                {
                    return NotFound(portfolioResult); 
                }

                return Ok(portfolioResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
