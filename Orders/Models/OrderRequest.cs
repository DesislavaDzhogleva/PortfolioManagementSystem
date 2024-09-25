using System.ComponentModel.DataAnnotations;

namespace Orders.Models
{
    public class OrderRequest
    {
        [Required]
        [MaxLength(50)]
        public string Ticker { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Side { get; set; }
    }
}
