using System.ComponentModel.DataAnnotations;

namespace Prices.Models
{
    public class StockInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Ticker { get; set; }

        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }
    }
}
