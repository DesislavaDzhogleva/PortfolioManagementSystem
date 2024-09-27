using Orders.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Orders.Models
{
    public class OrderRequest
    {
        [Required]
        [MaxLength(50)]
        public string Ticker { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [OrderSideValidation]
        public string Side { get; set; }
    }
}
