using System.ComponentModel.DataAnnotations;

namespace Prices.Data.Models
{
    public class StockEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ticker { get; set; }

        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }
    }
}
