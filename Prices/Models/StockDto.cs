using System.ComponentModel.DataAnnotations;

namespace Prices.Models
{
    public class StockDto
    {
        public string Ticker { get; set; }

        public string CompanyName { get; set; }
    }
}
