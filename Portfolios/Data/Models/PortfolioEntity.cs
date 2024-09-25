using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Portfolios.Data.Models
{
    public class PortfolioEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Ticker { get; set; }

        [Range(0, int.MaxValue)]
        public int NumberOfShares { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AveragePrice { get; set; }

        [NotMapped]
        public decimal TotalMarketValue => NumberOfShares * CurrentPrice;

        [NotMapped]
        public decimal TotalInvestment => AveragePrice * NumberOfShares;

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime UpdatedOn { get; set; }
    }
}
